using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonTesting.DAL.Model
{
    public class TestResult : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        public int CorrectQuestions { set; get; }

        [Required]
        [MaxLength(30)]
        public string ContestantName { set; get; } = null!;

        [Required]
        public DateTime CompletedOn { set; get; }

        public Guid TestId { set; get; }

        public virtual Test Test { set; get; } = null!;
    }
}
