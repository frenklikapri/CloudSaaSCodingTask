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

            name = name.ToLower();

            Expression<Func<Employee, bool>> filter = i => i.Name.ToLower().Contains(name)
                && i.Email.ToLower().Contains(email)
                && i.Department.ToLower().Contains(department);

            return filter;
        }
    }
}
