using Mission08_Team0101.Models;

namespace Mission08_Team0101.Data
{
    public interface ITaskRepository
    {
        IEnumerable<ToDoTask> GetAllTasks();
        IEnumerable<ToDoTask> GetIncompleteTasks();
        ToDoTask? GetTaskById(int id);
        void AddTask(ToDoTask task);
        void UpdateTask(ToDoTask task);
        void DeleteTask(int id);
        void SaveChanges();

        IEnumerable<Category> GetAllCategories();
    }
}