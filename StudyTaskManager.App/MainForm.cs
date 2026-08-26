using StudyTaskManager.Core;
using StudyTaskManager.Validation;

namespace StudyTaskManager.App;

public class MainForm : Form
{
    private readonly TaskService _taskService = new();

    private readonly TextBox txtTitle = new();
    private readonly TextBox txtSubject = new();
    private readonly DateTimePicker dtpDueDate = new();
    private readonly ListBox lstTasks = new();
    private readonly Label lblPending = new();

    public MainForm()
    {
        Text = "Study Task Manager";
        Width = 820;
        Height = 560;
        StartPosition = FormStartPosition.CenterScreen;

        BuildInterface();
        RefreshTasks();
    }

    private void BuildInterface()
    {
        var titleLabel = new Label { Text = "Task title", Left = 30, Top = 30, Width = 100 };
        txtTitle.SetBounds(140, 25, 240, 30);

        var subjectLabel = new Label { Text = "Subject", Left = 410, Top = 30, Width = 70 };
        txtSubject.SetBounds(480, 25, 250, 30);

        var dueLabel = new Label { Text = "Due date", Left = 30, Top = 80, Width = 100 };
        dtpDueDate.SetBounds(140, 75, 240, 30);
        dtpDueDate.Value = DateTime.Today.AddDays(1);

        var btnAdd = new Button { Text = "Add Task" };
        btnAdd.SetBounds(410, 72, 100, 35);
        btnAdd.Click += AddTask_Click;

        var btnComplete = new Button { Text = "Mark Completed" };
        btnComplete.SetBounds(520, 72, 130, 35);
        btnComplete.Click += MarkCompleted_Click;

        var btnDelete = new Button { Text = "Delete" };
        btnDelete.SetBounds(660, 72, 70, 35);
        btnDelete.Click += DeleteTask_Click;

        lstTasks.SetBounds(30, 140, 700, 300);

        lblPending.SetBounds(30, 460, 300, 30);

        var dllInfo = new Label
        {
            Text = "Dependencies: StudyTaskManager.Core.dll + StudyTaskManager.Validation.dll",
            Left = 330,
            Top = 460,
            Width = 410
        };

        Controls.AddRange(new Control[]
        {
            titleLabel, txtTitle, subjectLabel, txtSubject,
            dueLabel, dtpDueDate, btnAdd, btnComplete,
            btnDelete, lstTasks, lblPending, dllInfo
        });
    }

    private void AddTask_Click(object? sender, EventArgs e)
    {
        var validation = TaskValidator.Validate(
            txtTitle.Text,
            txtSubject.Text,
            dtpDueDate.Value);

        if (!validation.IsValid)
        {
            MessageBox.Show(
                validation.Message,
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        _taskService.AddTask(new TaskItem
        {
            Title = txtTitle.Text.Trim(),
            Subject = txtSubject.Text.Trim(),
            DueDate = dtpDueDate.Value.Date
        });

        txtTitle.Clear();
        txtSubject.Clear();
        dtpDueDate.Value = DateTime.Today.AddDays(1);

        RefreshTasks();
    }

    private void MarkCompleted_Click(object? sender, EventArgs e)
    {
        if (lstTasks.SelectedIndex < 0)
        {
            MessageBox.Show("Select a task first.");
            return;
        }

        _taskService.MarkCompleted(lstTasks.SelectedIndex);
        RefreshTasks();
    }

    private void DeleteTask_Click(object? sender, EventArgs e)
    {
        if (lstTasks.SelectedIndex < 0)
        {
            MessageBox.Show("Select a task first.");
            return;
        }

        _taskService.DeleteTask(lstTasks.SelectedIndex);
        RefreshTasks();
    }

    private void RefreshTasks()
    {
        lstTasks.Items.Clear();

        foreach (var task in _taskService.GetTasks())
            lstTasks.Items.Add(task);

        lblPending.Text = $"Pending tasks: {_taskService.GetPendingCount()}";
    }
}
