using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagerDBContext _context;

        public TasksController(TaskManagerDBContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult> GetTasks()
        {
            var taskList = await _context.Tasks.ToListAsync();
            return Ok(taskList);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody]TaskModel taskRequest)
        {
            await _context.Tasks.AddAsync(taskRequest);
            await _context.SaveChangesAsync();
            return Ok(taskRequest);
        }

        // GET: api/tasks/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTask([FromRoute]int id)
        {
            var _task= await _context.Tasks.FirstOrDefaultAsync(e => e.Id == id);
            if(_task != null)
            {
                return Ok(_task);
            }
            return NotFound();
        }

        // PUT: api/tasks/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskModel updateTask)
        {
            var _task = await _context.Tasks.FindAsync(id);

            if(_task != null)
            {
                _task.Title = updateTask.Title;
                _task.Description = updateTask.Description;
                _task.DueDate = updateTask.DueDate;

                await _context.SaveChangesAsync();
                return Ok(_task);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var _task = await _context.Tasks.FindAsync(id);
            if (_task != null)
            {
                _context.Tasks.Remove(_task);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            return NotFound();

        }
    }
}