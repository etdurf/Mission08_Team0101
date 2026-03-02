using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_Team0101.Data;
using Mission08_Team0101.Models;
using System.Linq;

namespace Mission08_Team0101.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // ADD or EDIT (same view)
        [HttpGet]
        public IActionResult Index(int? id)
        {
            ViewBag.Categories = new SelectList(_repo.GetAllCategories(), "CategoryId", "CategoryName");

            // Create
            if (id == null)
            {
                return View(new ToDoTask
                {
                    Completed = false
                });
            }

            // Edit
            var task = _repo.GetAllTasks().Single(x => x.TaskId == id);
            return View(task);
        }

        // Handle both Create + Update
        [HttpPost]
        public IActionResult Index(ToDoTask task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_repo.GetAllCategories(), "CategoryId", "CategoryName");
                return View(task);
            }

            if (task.TaskId == 0)
            {
                _repo.AddTask(task);     // Make sure your repo has AddTask
            }
            else
            {
                _repo.UpdateTask(task);
            }

            return RedirectToAction("Quadrants");
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
        public IActionResult Edit(int id) => RedirectToAction("Index", new { id });

        [HttpPost]
        public IActionResult Edit(ToDoTask task) => RedirectToAction("Index", task);
        
        
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
