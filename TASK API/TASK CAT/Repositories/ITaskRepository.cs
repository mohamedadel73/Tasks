using TASK_CAT.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync(string? status);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(int id, TaskUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
}