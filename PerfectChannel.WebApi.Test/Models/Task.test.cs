using NUnit.Framework;
using PerfectChannel.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfectChannel.WebApi.Test.Models
{
    [TestFixture]
    class TaskTest
    {
        [Test]
        public void ShouldCreateTask()
        {
            Task task = new Task();
            
            Assert.NotNull(task);
        }

        [Test]
        public void ShouldStoreParametersWithoutModifications()
        {
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending"};

            Assert.AreEqual(task.Id, 1);
            Assert.AreEqual(task.Description, "This is a test!");
            Assert.AreEqual(task.Status, "Pending");
        }

        [Test]
        public void ShouldBeValid()
        {
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Pending" };

            bool result = task.IsValid;

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeInvalidDueToEmptyDescription()
        {
            Task task = new Task { Id = 1, Description = "", Status = "Pending" };

            bool result = task.IsValid;

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidDueToNullDescription()
        {
            Task task = new Task { Id = 1, Description = null, Status = "Pending" };

            bool result = task.IsValid;

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidDueToEmptyStatus()
        {
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "" };

            bool result = task.IsValid;

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidDueToNullStatus()
        {
            Task task = new Task { Id = 1, Description = "This is a test!", Status = null };

            bool result = task.IsValid;

            Assert.IsFalse(result);
        }

        public void ShouldBeInvalidDueToInvalidStatus()
        {
            Task task = new Task { Id = 1, Description = "This is a test!", Status = "Open" };

            bool result = task.IsValid;

            Assert.IsFalse(result);
        }
    }
}
