using API_TimeTracker.Data;
using API_TimeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_TimeTracker.Models;
using System;


namespace MVC_TimeTracker.Controllers
{
    public class UserDataController : Controller
    {


        private readonly HttpClient _client;
        public UserDataController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApiHelper.BaseAddress);

        }

        [HttpGet]
        public async Task<IActionResult> UserData()
        {
            try
            {
                var projectsEndpoint = "https://localhost:44334/api/Task/projects";
                var locationsEndpoint = "https://localhost:44334/api/Task/locations";

                var projectsResponse = await _client.GetAsync(projectsEndpoint);
                var locationsResponse = await _client.GetAsync(locationsEndpoint);

                projectsResponse.EnsureSuccessStatusCode();
                locationsResponse.EnsureSuccessStatusCode();

                var projects = await projectsResponse.Content.ReadFromJsonAsync<List<ProjectModel>>();
                var locations = await locationsResponse.Content.ReadFromJsonAsync<List<LocationModel>>();

                ViewBag.Projects = projects;
                ViewBag.Locations = locations;
                return View();
            }
            catch (HttpRequestException ex)
            {
                return View("Error");
            }
        }


        [Route("Task/SaveTask")]
        [HttpPost]
        public async Task<IActionResult> SaveTask(TaskViewModel model)
        {
            try
            {
                string? userName = TempData["UserName"] as string;
                TempData.Keep();
                if (string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError(string.Empty, "User information not found.");
                    return View("UserData");
                }
                var getUserIdResponse = await _client.GetAsync(_client.BaseAddress + "/Task/GetUserId?userName=" + userName);

                if (!getUserIdResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve UserId for the given userName.");
                    return View("UserData");
                }
                UserViewModel userdata = await getUserIdResponse.Content.ReadAsAsync<UserViewModel>();
                model.UserId = userdata.UserId;
                var Response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Task/SaveTask", model);

                if (Response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Task Added Successfully!";
                   
                    TempData["Permision"] = Convert.ToInt32(userdata.permission);
                    return RedirectToAction("UserData", "UserData");
                }
                else
                {

                    var errorMessage = await Response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View("UserData");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Failed to save the task. Try again after some time.");
                return View("UserData");
            }
        }

        [HttpGet]
        public IActionResult ViewReports()
        {
            return View();
        }

    }
}
