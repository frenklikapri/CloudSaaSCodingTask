using System.ComponentModel.DataAnnotations;

namespace CloudSaaSCodingTask.Core.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Department { get; set; }
    }
}
