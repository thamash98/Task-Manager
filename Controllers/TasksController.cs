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

    }
}