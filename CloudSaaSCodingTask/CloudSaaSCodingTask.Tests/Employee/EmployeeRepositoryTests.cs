using CloudSaaSCodingTask.Core.Entities;
using CloudSaaSCodingTask.Infrastructure.Data;
using CloudSaaSCodingTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CloudSaaSCodingTask.Tests.Employee
{
    public class EmployeeRepositoryTests
    {
        [TestCase]
        public async Task GetEmployees_Should_Work()
        {
            var list = new List<Core.Entities.Employee>();
            var mockEmployees = new Mock<DbSet<Core.Entities.Employee>>();

            for (int i = 0; i < 30; i++)
            {
                list.Add(new Core.Entities.Employee
                {
                    DateOfBirth = DateTime.Now,
                    Department = $"Department {i}",
                    Email = $"email{i}@gmail.com",
                    Name = $"Employee {i}"
                });
            }
            mockEmployees.Object.AddRange(list);

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Set<Core.Entities.Employee>()).Returns(mockEmployees.Object);

            var employeeRepository = new Mock<EFGenericRepository<Core.Entities.Employee>>(mockContext.Object);
            employeeRepository.Setup(m => m.GetAsQueryable()).Returns(list.AsQueryable());

            var repo = employeeRepository.Object;
            var items = await repo.GetItemsAsync(new Core.Common.PaginationParameters
            {
                Page = 0,
                PageSize = 100
            }, null);

            Assert.AreEqual(30, items.TotalCount);
        }
    }
}
