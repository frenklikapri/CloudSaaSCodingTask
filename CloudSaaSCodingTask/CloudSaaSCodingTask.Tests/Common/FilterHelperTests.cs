using CloudSaaSCodingTask.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Tests.Common
{
    public class FilterHelperTests
    {
        [TestCaseSource(nameof(SingleEntityFilterCases))]
        public void Filter_Helper_Should_Work_One_Entity(Core.Entities.Employee employee, string name, string email, string department,
            bool passesSearch)
        {
            var filter = EmployeeSearchHelper.BuildFilter(name, email, department);
            var result = filter.Compile()(employee);

            Assert.AreEqual(passesSearch, result);
        }

        [TestCase]
        public void Filter_Helper_Null_Not_Throwing_Exception()
        {
            var filter = EmployeeSearchHelper.BuildFilter(null, null, null);
            Assert.Pass("Test passed");
        }


        public static object[] SingleEntityFilterCases =
        {
            new object[] { new Core.Entities.Employee
            {
                Name = "Employee test 1",
                Email = "email@gmail.com",
                Department = "dep123"
            }, "Employee test 1", "email@gmail.com", "dep123", true },
            new object[] { new Core.Entities.Employee
            {
                Name = "Employee test 1",
                Email = "email@gmail.com",
                Department = "dep123"
            }, "empl", "gmai", "p123", true },
            new object[] { new Core.Entities.Employee
            {
                Name = "Employee test 1",
                Email = "email@gmail.com",
                Department = "dep123"
            }, "empl", "gmai", "321", false },
            new object[] { new Core.Entities.Employee
            {
                Name = "Employee test 1",
                Email = "email@gmail.com",
                Department = "dep123"
            }, "empl", "gmail.com", "dep123", true },
        };
    }
}
