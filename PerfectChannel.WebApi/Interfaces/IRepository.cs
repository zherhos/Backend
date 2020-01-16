using PerfectChannel.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PerfectChannel.WebApi.Interfaces
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();
        public T GetById(Expression<Func<T, bool>> expression);
        public int Create(T task);
        public int Update(T task);
        public int Delete(T task);
    }
}
    