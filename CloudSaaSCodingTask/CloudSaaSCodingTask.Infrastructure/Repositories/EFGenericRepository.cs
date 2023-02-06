using CloudSaaSCodingTask.Core.Common;
using CloudSaaSCodingTask.Core.Entities;
using CloudSaaSCodingTask.Core.Repositories;
using CloudSaaSCodingTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CloudSaaSCodingTask.Infrastructure.Repositories
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public EFGenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RepositoryActionResult<T>> DeleteAsync(Guid id)
        {
            var entity = await _dbContext
                .Set<T>()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null)
                return new RepositoryActionResult<T>
                {
                    NotFound = true
                };

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return new RepositoryActionResult<T>
            {
                Success = true
            };
        }

        public async Task<RepositoryActionResult<T>> EditAsync(T entity)
        {
            var foundEntity = await _dbContext
                .Set<T>()
                .FirstOrDefaultAsync(t => t.Id == entity.Id);

            if (foundEntity is null)
                return new RepositoryActionResult<T>
                {
                    NotFound = true
                };

            _dbContext.Entry(foundEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();

            return new RepositoryActionResult<T>
            {
                Success = true
            };
        }

        public async Task<RepositoryActionResult<T>> GetByIdAsync(Guid id)
        {
            var foundEntity = await _dbContext
                .Set<T>()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (foundEntity is null)
                return new RepositoryActionResult<T>
                {
                    NotFound = true
                };
            else
                return new RepositoryActionResult<T>
                {
                    Success = true,
                    Entity = foundEntity
                };
        }

        public async Task<PaginatedResult<T>> GetItemsAsync(PaginationParameters paginationParameters, Expression<Func<T, bool>> filter)
        {
            var query = _dbContext
                .Set<T>()
                .AsQueryable();

            if (filter is not null)
            {
                query = query
                    .Where(filter);
            }

            var items = await query
                .Skip((paginationParameters.Page - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            var total = await query.CountAsync();

            return new PaginatedResult<T>
            {
                Items = items,
                TotalCount = total
            };
        }
    }
}
