using Microsoft.AspNetCore.Mvc;
using System;
using TASK_CAT.Models;
using TASK_CAT.Repositories;

namespace TASK_CAT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        private bool IsValidStatus(string? status) =>
            string.IsNullOrWhiteSpace(status) ||
            status is "pending" or "completed";

        private bool IsValidPriority(string? priority) =>
            string.IsNullOrWhiteSpace(priority) ||
            priority is "low" or "medium" or "high";

        // POST: /tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskCreateDto dto)
        {
            if (!IsValidStatus(dto.Status))
                return BadRequest(new { error = "Status must be 'pending' or 'completed'" });

            if (!IsValidPriority(dto.Priority))
                return BadRequest(new { error = "Priority must be 'low', 'medium', or 'high'" });

            var task = dto.ToTaskItem(0); // ID will be set in repo
            var createdTask = await _repository.AddAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // GET: /tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks([FromQuery] string? status)
        {
            if (!string.IsNullOrWhiteSpace(status) && !IsValidStatus(status))
                return BadRequest(new { error = "Invalid status filter" });

            var tasks = await _repository.GetAllAsync(status);
            return Ok(tasks);
        }

        // GET: /tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // PATCH: /tasks/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(int id, TaskUpdateDto dto)
        {
            if (dto.Status != null && !IsValidStatus(dto.Status))
                return BadRequest(new { error = "Status must be 'pending' or 'completed'" });

            if (dto.Priority != null && !IsValidPriority(dto.Priority))
                return BadRequest(new { error = "Priority must be 'low', 'medium', or 'high'" });

            var updatedTask = await _repository.UpdateAsync(id, dto);
            if (updatedTask == null)
                return NotFound();

            return Ok(updatedTask);
        }

        // DELETE: /tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
