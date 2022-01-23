using AnonTesting.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonTesting.DAL.Model
{
    public class User : IdentityUser<Guid>, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id 
        { 
            get => base.Id;
            set => base.Id = value; 
        }

        public virtual ICollection<Test> CreatedTests { set; get; } = null!;
    }
}
