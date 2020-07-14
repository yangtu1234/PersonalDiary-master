using Microsoft.EntityFrameworkCore;
using PersonalDiary.Models;
using PersonalDiary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonalDiary.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public PersonalDiaryContext PersonalDiaryContext { get; set; }
        public BaseRepository(PersonalDiaryContext personalDiaryContext)
        {
            this.PersonalDiaryContext = personalDiaryContext;
        }

        public async Task AddEntity(T entity)
        {
            await PersonalDiaryContext.Set<T>().AddAsync(entity);
        }

        public void DeleteEntity(T entity)
        {
            PersonalDiaryContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void EditEntity(T entity)
        {
            PersonalDiaryContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<bool> ExistEntity(Expression<Func<T, bool>> whereLamda)
        {
            return await PersonalDiaryContext.Set<T>().AnyAsync(whereLamda);
        }

        public IQueryable<T> GetEntityForPage<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            var temp = PersonalDiaryContext.Set<T>().Where(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return temp;
        }

        public IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLamda)
        {
            return PersonalDiaryContext.Set<T>().Where(whereLamda);
        }

        public async Task<bool> SaveChanges()
        {
            return await this.PersonalDiaryContext.SaveChangesAsync() > 0;
        }
    }
}
