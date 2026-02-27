using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0101.Data;
using Mission08_Team0101.Models;

namespace Mission08_Team0101.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quadrants()
        {
            var tasks = _repo.GetAllTasks();
            ViewBag.Categories = _repo.GetAllCategories();
            return View(tasks);
        }
        // ------------ COMPLETE TASK --------------
        public IActionResult Complete(int id)
        {
            var taskToComplete = _repo.GetAllTasks()
                .Single(x => x.TaskId == id);
    
            taskToComplete.Completed = true;
            _repo.UpdateTask(taskToComplete);
            return RedirectToAction("Quadrants");
        }
        
        
        // ------------ EDIT TASK --------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var taskToEdit = _repo.GetAllTasks()
                .Single(x => x.TaskId == id);
    
            return View(taskToEdit); // EDIT when implemented
        }
        
        [HttpPost]
        public IActionResult Edit(ToDoTask task)
        {
            _repo.UpdateTask(task);
            return RedirectToAction("Quadrants");
        }
        
        // ------------ DELETE TASK --------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _repo.GetAllTasks()
                .Single(x => x.TaskId == id);
    
            return View(taskToDelete);
        }
        
        [HttpPost]
        public IActionResult Delete(int id, string? returnUrl)
        {
            if (id != 0)
            {
                _repo.DeleteTask(id);
            }
    
            return RedirectToAction(returnUrl ?? "Quadrants");
        }
        
        
    }
}
