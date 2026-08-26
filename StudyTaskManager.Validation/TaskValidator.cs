namespace StudyTaskManager.Validation;

public static class TaskValidator
{
    public static (bool IsValid, string Message) Validate(
        string title,
        string subject,
        DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            return (false, "Task title is required.");

        if (title.Trim().Length < 3)
            return (false, "Task title must contain at least 3 characters.");

        if (string.IsNullOrWhiteSpace(subject))
            return (false, "Subject is required.");

        if (dueDate.Date < DateTime.Today)
            return (false, "Due date cannot be in the past.");

        return (true, string.Empty);
    }
}
