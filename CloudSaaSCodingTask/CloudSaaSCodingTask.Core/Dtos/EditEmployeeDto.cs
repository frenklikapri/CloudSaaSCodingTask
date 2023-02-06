using System.ComponentModel.DataAnnotations;

namespace CloudSaaSCodingTask.Core.Dtos
{
    public record EditEmployeeDto(Guid Id, [Required] string Name, [Required][EmailAddress] string Email, DateTime DateOfBirth, [Required] string Department);

}
