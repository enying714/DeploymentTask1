namespace StudyTaskManager.Core;

public class TaskItem
{
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        string status = IsCompleted ? "Completed" : "Pending";
        return $"{Title} | {Subject} | Due: {DueDate:dd/MM/yyyy} | {status}";
    }
}
