using System;
using System.ComponentModel.DataAnnotations;

namespace TASK_CAT.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; } = string.Empty;

        public string Status { get; set; } = "pending";

        public string Priority { get; set; } = "medium";

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
