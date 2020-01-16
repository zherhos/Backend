using PerfectChannel.WebApi.Interfaces;
using PerfectChannel.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PerfectChannel.WebApi.Test.MockClasses
{
    interface INumOfRowsGetSetAble
    {
        public int NumOfRowsAffectedMock { get; set; }
    }

    interface IBreakable
    {
        public bool Broken { get; set; }
    }

    public class RepositoryMock : IRepository<ITask>, INumOfRowsGetSetAble, IBreakable
    {
        public int NumOfRowsAffectedMock { get; set; }
        public bool Broken { get; set; }

        public int Create(ITask task)
        {
            return Broken ? 0 : NumOfRowsAffectedMock;
        }

        public IQueryable<ITask> GetAll()
        {
            return GenerateTasks();
        }

        public ITask GetById(Expression<Func<ITask, bool>> expression)
        {
            IQueryable<ITask> list = GenerateTasks();
            ITask task = list.Where(expression).FirstOrDefault();

            return task;
        }

        public int Update(ITask task)
        {
            return Broken ? 0 : NumOfRowsAffectedMock;
        }

        public int Delete(ITask task)
        {
            throw new NotImplementedException();
        }

        private IQueryable<ITask> GenerateTasks()
        {
            List<Task> list = new List<Task>();
            int cont = 0;

            while (cont < NumOfRowsAffectedMock)
            {
                list.Add(new Task { Id = cont, Description = cont.ToString(), Status = cont.ToString() }) ;
                cont++;
            }

            return list.AsQueryable();
        }
    }
}