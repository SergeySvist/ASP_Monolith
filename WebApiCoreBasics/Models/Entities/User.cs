using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCoreBasics.Models.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisteredAt { get; set; }

        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
        public Account? Account { get; set; }

    }
}
