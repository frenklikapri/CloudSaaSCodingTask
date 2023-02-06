using CloudSaaSCodingTask.Core.Entities;
using CloudSaaSCodingTask.Infrastructure.Data;
using Faker;
using Microsoft.EntityFrameworkCore;

namespace CloudSaaSCodingTask.Web.Seed
{
    public static class SeedData
    {
        public static void Seed(this ApplicationDbContext dbContext)
        {
            if (!dbContext.Employees.Any())
            {
                dbContext.Employees.AddRange(GetFakeEmployees());
                dbContext.SaveChanges();
            }
        }

        public static List<Employee> GetFakeEmployees()
        {
            var employees = new List<Employee>();
            for (int i = 0; i < 2000; i++)
            {
                employees.Add(new Core.Entities.Employee
                {
                    Name = Faker.Name.FullName(NameFormats.WithPrefix),
                    Email = Faker.Internet.Email(),
                    DateOfBirth = Faker.Identification.DateOfBirth(),
                    Department = Faker.Company.Name(),
                });
            }
            return employees;
        }
    }
}
