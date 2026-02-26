using Microsoft.EntityFrameworkCore;
using Mission08_Team0101.Models;

namespace Mission08_Team0101.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDoTask> GetAllTasks()
        {
            return _context.Tasks
                .Include(t => t.Category)
                .ToList();
        }

        public IEnumerable<ToDoTask> GetIncompleteTasks()
        {
            return _context.Tasks
                .Include(t => t.Category)
                .Where(t => !t.Completed)
                .ToList();
        }

        public ToDoTask? GetTaskById(int id)
        {
            return _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TaskId == id);
        }

        public void AddTask(ToDoTask task)
        {
            _context.Tasks.Add(task);
            SaveChanges();
        }

        public void UpdateTask(ToDoTask task)
        {
            _context.Tasks.Update(task);
            SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}