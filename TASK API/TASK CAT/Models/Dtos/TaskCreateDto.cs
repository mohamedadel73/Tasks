using System.ComponentModel.DataAnnotations;
using TASK_CAT.Models;

public class TaskCreateDto
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(1, ErrorMessage = "Title cannot be empty")]
    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = "pending";

    public string Priority { get; set; } = "medium";

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public List<string>? Tags { get; set; }

    public TaskItem ToTaskItem(int id)
    {
        return new TaskItem
        {
            Id = id,
            Title = Title,
            Status = Status,
            Priority = Priority,
            Description = Description,
            DueDate = DueDate,
            Tags = Tags ?? new List<string>()
        };
    }
}