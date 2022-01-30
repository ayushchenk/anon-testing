using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonTesting.DAL.Model
{
    public class Question : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public QuestionType QuestionType { set; get; }

        [Required]
        public string Content { set; get; } = null!;

        public Guid TestId { set; get; }

        public virtual Test Test { set; get; } = null!;

        public virtual ICollection<Answer> Answers { set; get; } = null!;
    }

    public enum QuestionType : byte
    {
        SingleAnswer = 0,
        MultipleAnswers = 1,
        StringAnswer = 2
    }
}
