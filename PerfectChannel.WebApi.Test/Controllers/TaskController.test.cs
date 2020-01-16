// Add your code here and rename the file accordingly.

namespace NUnit.Tests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using PerfectChannel.WebApi.Controllers;
    using PerfectChannel.WebApi.Interfaces;
    using PerfectChannel.WebApi.Models;
    using PerfectChannel.WebApi.Test.MockClasses;

    [TestFixture]
    public class TaskControllerTest
    {
        public IRepository<ITask> taskRepository;
        public TaskController controller;

        public TaskControllerTest()
        {
            this.taskRepository = new RepositoryMock();
            this.controller = new TaskController(taskRepository);
        }

        [Test]
        public void ShouldCreateTaskController()
        {
            Assert.NotNull(controller);
        }

        [Test]
        public void ShouldReturnOkResponseToGetAllRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 1;

            var result = controller.AllTaskRequest();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ShouldReturnNotFoundResponseToGetAllRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 0;

            var result = controller.AllTaskRequest();

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void ShouldReturnNotFoundResultToTaskByIdRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 4;

            var result = controller.TaskByIdRequest(5);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void ShouldReturnOkObjectResultToTaskByIdRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;

            var result = controller.TaskByIdRequest(2);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ShouldReturnBadRequestToNewTaskCreationRequestDueToMissingDescription()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;
            Task task = new Task { Id = 1, Description = "", Status = "Completed" };

            var result = controller.NewTaskCreationRequest(task);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnBadRequestToNewTaskCreationRequestDueToMissingStatus()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "" };

            var result = controller.NewTaskCreationRequest(task);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnOkObjectResultToNewTaskCreationRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending" };

            var result = controller.NewTaskCreationRequest(task);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ShouldReturnNotFoundResultToNewTaskCreationRequest()
        {
            ((IBreakable)taskRepository).Broken = true;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending" };

            var result = controller.NewTaskCreationRequest(task);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void ShouldReturnBadRequestToUpdateTaskRequestDueToEmptyDescription()
        {
            ((IBreakable)taskRepository).Broken = false;
            Task task = new Task { Id = 1, Description = "", Status = "Completed" };
            
            var result = controller.UpdateTaskRequest(task);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnBadRequestToUpdateTaskRequestDueToEmptyStatus()
        {
            ((IBreakable)taskRepository).Broken = false;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "" };

            var result = controller.UpdateTaskRequest(task);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnOkObjectResultToUpdateTaskRequest()
        {
            ((IBreakable)taskRepository).Broken = false;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 5;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending" };

            var result = controller.UpdateTaskRequest(task);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ShouldReturnNotFoundResultToUpdateTaskRequest()
        {
            ((IBreakable)taskRepository).Broken = true;
            ((INumOfRowsGetSetAble)taskRepository).NumOfRowsAffectedMock = 0;
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending" };

            var result = controller.UpdateTaskRequest(task);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}