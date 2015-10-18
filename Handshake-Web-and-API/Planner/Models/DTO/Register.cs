using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class Register
    {
        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Type { get; set; }

        [DataType(DataType.Upload)]
        public string Resume { get; set; }

        public string tagline { get; set; }

        public string fullName { get; set; }
    }
}