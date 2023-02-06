using CloudSaaSCodingTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Core.Helpers
{
    public static class EmployeeSearchHelper
    {
        public static Expression<Func<Employee, bool>> BuildFilter(string name, string email, string department)
        {
            if (name is null)
                name = string.Empty;
            if (email is null)
                email = string.Empty;
            if (department is null)
                department = string.Empty;

            Expression<Func<Employee, bool>> filter = i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                && i.Email.Contains(email, StringComparison.OrdinalIgnoreCase)
                && i.Department.Contains(department, StringComparison.OrdinalIgnoreCase);
            //Expression<Func<Employee, bool>> filter = null;

            //if (!string.IsNullOrEmpty(name))
            //{
            //    filter = i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase);
            //}

            //if (!string.IsNullOrEmpty(email))
            //{
            //    if (filter is null)
            //    {
            //        filter = i => i.Email.Contains(email, StringComparison.OrdinalIgnoreCase);
            //    }
            //    else
            //    {
            //        var compiled = filter.Compile();
            //        filter = i => compiled(i) && i.Email.Contains(email, StringComparison.OrdinalIgnoreCase);
            //    }
            //}

            //if (!string.IsNullOrEmpty(department))
            //{
            //    if (filter is null)
            //    {
            //        filter = i => i.Department.Contains(department, StringComparison.OrdinalIgnoreCase);
            //    }
            //    else
            //    {
            //        var compiled = filter.Compile();
            //        filter = i => compiled(i) && i.Department.Contains(department, StringComparison.OrdinalIgnoreCase);
            //    }
            //}

            return filter;
        }
    }
}
