using CRUDusingwebapi.DAL;
using CRUDusingwebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDusingwebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly MyAppDbContext _Contex;
        public ProductController(MyAppDbContext contex)
        {
            _Contex = contex;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _Contex.Products.ToList();
                if (products.Count == 0)
                {
                    return NotFound("Products not avilable.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _Contex.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product details not found with id{id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _Contex.Add(model);
                _Contex.SaveChanges();
                return Ok("Product Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(Product model)
        {
            if(model == null || model.Id == 0)
            {
                if(model == null)
                {
                    return BadRequest("Model data is invalid.");
                }
                else if(model.Id == 0)
                {
                    return BadRequest($"Product Id {model.Id}");
                }
            }
            try
            {
                var product = _Contex.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"product not found with id{model.Id}");
                }
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _Contex.SaveChanges();
                return Ok("Product details updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _Contex.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id{id}");
                }
                _Contex.Products.Remove(product);
                _Contex.SaveChanges();
                return Ok("product details deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
