using Microsoft.AspNetCore.Mvc;

namespace API_TimeTracker.Interfaces
{
    public interface ITaskController
    {
        IActionResult GetProjects();
        IActionResult GetLocations();
        IActionResult GetUsernames();
        IActionResult SaveTask([FromBody] TaskModel model);
        IActionResult GetAllTasks();
        IActionResult GetUserPermission(string userName);
        IActionResult GetUserId(string userName);
        IActionResult GetUserNameById(int userid);
        IActionResult GetLoactionNameById(int locationid);
        IActionResult GetProjectNameById(int projectid);
        IActionResult GetTasksByUserId(int userId, DateTime selectDate);


    }
}
