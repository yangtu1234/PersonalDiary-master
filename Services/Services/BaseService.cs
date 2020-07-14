using PersonalDiary.Repository.IRepository;
using PersonalDiary.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonalDiary.Services.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public IBaseRepository<T> CurrentRepository { get; set; }
        public Task AddEntity(T entity)
        {
            return CurrentRepository.AddEntity(entity);
        }

        public void DeleteEntity(T entity)
        {
            CurrentRepository.DeleteEntity(entity);
        }

        public void EditEntity(T entity)
        {
            CurrentRepository.EditEntity(entity);
        }

        public Task<bool> ExistEntity(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentRepository.ExistEntity(whereLamda);
        }

        public IQueryable<T> GetEntityForPage<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            return CurrentRepository.GetEntityForPage(pageIndex, pageSize, out totalCount, whereLambda, orderByLambda, isAsc);
        }

        public IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentRepository.GetEntitys(whereLamda);
        }

        public Task<bool> SaveChanges()
        {
            return CurrentRepository.SaveChanges();
        }

    }
}
