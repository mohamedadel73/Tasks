using System;
using System.Collections.Generic;
using System.Linq;
using TASK_CAT.Models;

namespace TASK_CAT.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public async Task<IEnumerable<TaskItem>> GetAllAsync(string? status)
        {
            await Task.CompletedTask; // Simulate async
            if (string.IsNullOrWhiteSpace(status))
                return _tasks;

            return _tasks.Where(t => t.Status == status);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            await Task.CompletedTask;
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            await Task.CompletedTask;
            task.Id = _nextId++;
            _tasks.Add(task);
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskUpdateDto updateDto)
        {
            await Task.CompletedTask;
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return null;

            if (updateDto.Title != null) task.Title = updateDto.Title;
            if (updateDto.Status != null) task.Status = updateDto.Status;
            if (updateDto.Priority != null) task.Priority = updateDto.Priority;
            if (updateDto.Description != null) task.Description = updateDto.Description;
            if (updateDto.DueDate.HasValue) task.DueDate = updateDto.DueDate;
            if (updateDto.Tags != null) task.Tags = updateDto.Tags;

            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.CompletedTask;
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;

            _tasks.Remove(task);
            return true;
        }
    }
}
