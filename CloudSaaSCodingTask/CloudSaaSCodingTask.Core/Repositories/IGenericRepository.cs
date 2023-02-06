using CloudSaaSCodingTask.Core.Common;
using CloudSaaSCodingTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<RepositoryActionResult<T>> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task<RepositoryActionResult<T>> EditAsync(T entity);
        Task<RepositoryActionResult<T>> DeleteAsync(Guid id);
        Task<PaginatedResult<T>> GetItemsAsync(PaginationParameters paginationParameters,
             Expression<Func<T, bool>> filter);
    }
}
