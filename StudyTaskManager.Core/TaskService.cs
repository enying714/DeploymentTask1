namespace StudyTaskManager.Core;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();

    public IReadOnlyList<TaskItem> GetTasks() => _tasks.AsReadOnly();

    public void AddTask(TaskItem task) => _tasks.Add(task);

    public void MarkCompleted(int index)
    {
        if (index >= 0 && index < _tasks.Count)
            _tasks[index].IsCompleted = true;
    }

    public void DeleteTask(int index)
    {
        if (index >= 0 && index < _tasks.Count)
            _tasks.RemoveAt(index);
    }

    public int GetPendingCount() => _tasks.Count(t => !t.IsCompleted);
}
