using PersonalDiary.Models;
using PersonalDiary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonalDiary.Services.IServices
{
    public interface IBaseService<T> where T:class
    {
        IBaseRepository<T> CurrentRepository { get; set; }

        IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLamda);
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> GetEntityForPage<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc);

        void DeleteEntity(T entity);

        void EditEntity(T entity);

        Task AddEntity(T entity);

        Task<bool> ExistEntity(Expression<Func<T, bool>> whereLamda);

        Task<bool> SaveChanges();
    }
}
