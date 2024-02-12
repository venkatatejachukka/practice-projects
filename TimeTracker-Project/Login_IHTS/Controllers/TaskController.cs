
using API_TimeTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API_TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase, ITaskController
    {
        private DataContext _context;

        public TaskController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _context.PROJECTNAMES.ToList();
                return Ok(projects);
            }
            catch (Exception)
            {
                return BadRequest("locations not found");
            }
        }

        [HttpGet("locations")]
        public IActionResult GetLocations()
        {
            try
            {
                var locations = _context.LOCATIONS.ToList();
                return Ok(locations);
            }
            catch (Exception)
            {
                return BadRequest("locations not found");
            }
        }

        [HttpGet("GetUsernames")]
        public IActionResult GetUsernames()
        {
            try
            {
                var usernames = _context.USERDETAILS.Select(u => u.UserName).ToList();
                return Ok(usernames);
            }
            catch (Exception)
            {
                return BadRequest("Usernames not found");
            }
        }

        [HttpPost("SaveTask")]
        public IActionResult SaveTask([FromBody] TaskModel model)
        {
            try
            {
                _context.TASKDETAILS.Add(model);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Failed to save the task.");
            }
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            try
            {
                var tasks = _context.TASKDETAILS.ToList();
                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest("Error fetching all tasks");
            }
        }

        [HttpGet("GetUserPermission")]
        public IActionResult GetUserPermission(string userName)
        {
            try
            {
                var user = _context.USERDETAILS.Where(u => u.UserName == userName).FirstOrDefault();
                return (user == null ? NotFound("user not found") : Ok(user.permission));
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving user permission");
            }
        }

        [HttpGet("GetUserId")]
        public IActionResult GetUserId(string userName)
        {
            try
            {
                var user = _context.USERDETAILS.Where(u => u.UserName == userName).FirstOrDefault();
                return (user == null ? NotFound("user not found") : Ok(user));
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving user ID");
            }
        }

        [HttpGet("GetUserNameById")]
        public IActionResult GetUserNameById(int userid)
        {
            var UserName = _context.USERDETAILS.Where(u => u.UserId == userid).FirstOrDefault();
            return (UserName == null ? NotFound("UserName not found") : Ok(UserName.UserName));
        }

        [HttpGet("GetLoactionNameById")]
        public IActionResult GetLoactionNameById(int locationid)
        {
            var location = _context.LOCATIONS.Where(l => l.LocationId == locationid).FirstOrDefault();
            return (location == null ? NotFound("Location not found") : Ok(location.LocationName));
        }

        [HttpGet("GetProjectNameById")]
        public IActionResult GetProjectNameById(int projectid)
        {
            var project = _context.PROJECTNAMES.Where(p => p.ProjectId == projectid).FirstOrDefault();
            return (project == null ? NotFound("project not found") : Ok(project.ProjectName));
        }

        [HttpGet("GetTasksByUserId")]
        public IActionResult GetTasksByUserId(int userId, DateTime selectDate)
        {
            try
            {
                var tasks = _context.TASKDETAILS.Where(t => t.UserId == userId && t.CREATIONDATE == DateOnly.FromDateTime(selectDate.Date)).ToList();
                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest("Error fetching tasks by user ID");
            }
        }
       
    }
}




