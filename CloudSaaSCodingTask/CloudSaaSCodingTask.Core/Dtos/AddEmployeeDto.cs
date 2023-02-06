using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Core.Dtos
{
    public record AddEmployeeDto([Required] string Name, [Required] [EmailAddress] string Email, DateTime DateOfBirth, [Required] string Department);
}
