using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonTesting.DAL.Model
{
    public class Test : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }

        [Required]
        [StringLength(100)]
        public string Title { set; get; } = string.Empty;

        public Guid UserId { set; get; }

        public virtual User User { set; get; } = null!;

        public virtual ICollection<Question> Questions { set; get; } = null!;
    }
}
