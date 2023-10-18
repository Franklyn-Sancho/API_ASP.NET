

using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Posts
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        // Chave estrangeira para a tabela Users
        public Guid AuthorId { get; set; }

    }
}