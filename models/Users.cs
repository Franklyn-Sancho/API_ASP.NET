

using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Users
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }

    }
}