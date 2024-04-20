using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<Task> tasks = new List<Task>
        {
            new Task { Id = 1, Title = "Task 1", Description = "Description for Task 1", DueDate = DateTime.Now.AddDays(1) },
            new Task { Id = 2, Title = "Task 2", Description = "Description for Task 2", DueDate = DateTime.Now.AddDays(2) }
        };

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest("Task object is null");
            }

            task.Id = tasks.Count + 1; // Assigning a new ID
            tasks.Add(task);
            return CreatedAtRoute("GetTaskById", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] Task task)
        {
            if (task == null || task.Id != id)
            {
                return BadRequest("Invalid task object");
            }

            var existingTask = tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound("Task not found");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete == null)
            {
                return NotFound("Task not found");
            }

            tasks.Remove(taskToDelete);
            return NoContent();
        }
    }
}