using PerfectChannel.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PerfectChannel.WebApi.Models
{
    public class TaskRepository : IRepository<ITask>
    {
        #region Variables
        private TaskDBContext dbContext;
        #endregion

        #region Constructor
        public TaskRepository(TaskDBContext context)
        {
            this.dbContext = context;
        }
        #endregion

        #region IRepository implementation
        public IQueryable<ITask> GetAll() 
        {
            return dbContext.Tasks;
        }
        public ITask GetById(Expression<Func<ITask, bool>> expression)
        {
            return dbContext.Tasks.Where<ITask>(expression).FirstOrDefault<ITask>();
        }
        public int Create(ITask task) 
        {
            dbContext.Tasks.Add(task as Task);

            return dbContext.SaveChanges();
        }

        public int Update(ITask task) 
        {
            ITask savedTask = GetById(x => x.Id == task.Id);

            if(savedTask == null)
            {
                return 0;
            }
            
            CopyTaskOverSavedTask(task, savedTask);

            return dbContext.SaveChanges();
        }

        public int Delete(ITask task)
        {
            ITask savedTask = GetById(x => x.Id == task.Id);

            if (savedTask == null)
            {
                return 0;
            }

            dbContext.Remove<ITask>(task);

            return dbContext.SaveChanges();
        }
        #endregion

        #region Private methods
        private void CopyTaskOverSavedTask(ITask task, ITask savedTask)
        {
            savedTask.Description = task.Description;
            savedTask.Status = task.Status;
        }
        #endregion
    }
}
