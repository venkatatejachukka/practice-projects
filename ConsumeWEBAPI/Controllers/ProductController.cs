using ConsumeWEBAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsumeWEBAPI.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddres = new Uri("https://localhost:44345/api");
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductViewModel>>(data)!;
            }
            return View(productList);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json" );
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/product/Post", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccesMessage"] = "Product Created";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit( int id)
        {
            ProductViewModel product = new ProductViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductViewModel>(data)!;
                return View(product);
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/product/Put", content).Result;
            if (response.IsSuccessStatusCode)
            {
                //TempData["SuccessMessage"] = "Product Updated";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProductViewModel product = new ProductViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductViewModel>(data)!;
                return View(product);
            }
            return View(product);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress 
                + "/product/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Product Deleted";
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}