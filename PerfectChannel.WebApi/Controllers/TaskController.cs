using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PerfectChannel.WebApi.Interfaces;
using PerfectChannel.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectChannel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IRepository<ITask> taskRepository;

        public TaskController(IRepository<ITask> dbContext)
        {
            this.taskRepository = dbContext;
        }

        #region HttpGet Methods
        [HttpGet]
        public IActionResult AllTaskRequest()
        {
            IQueryable<ITask> tasks = taskRepository.GetAll();

            if(tasks.Count() == 0)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public IActionResult TaskByIdRequest(int id)
        {
            ITask task = taskRepository.GetById(x => x.Id == id);

            if(task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        #endregion

        #region HttpPost Methods

        [HttpPost]
        public IActionResult NewTaskCreationRequest([FromBody]Task task)
        {
            if (!task.IsValid)
            {
                return BadRequest();
            }

            if (taskRepository.Create(task) > 0)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region HttpPut Methods
        [HttpPut]
        public IActionResult UpdateTaskRequest([FromBody]Task task)
        {
            if (!task.IsValid)
            {
                return BadRequest();
            }

            if (taskRepository.Update(task) > 0)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion
    }
}