using CloudSaaSCodingTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Core.Common
{
    public class RepositoryActionResult<T> where T : BaseEntity
    {
        public bool Success { get; set; }
        public bool NotFound { get; set; }
        public T Entity { get; set; }
    }
}
