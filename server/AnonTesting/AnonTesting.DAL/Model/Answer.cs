using AnonTesting.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonTesting.DAL.Model
{
    public class Answer : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public bool IsCorrect { set; get; }

        [Required]
        public string Content { set; get; } = null!;

        public Guid QuestionId { set; get; }

        public virtual Question Question { set; get; } = null!;
    }
}
