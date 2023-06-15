using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class NumberVilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }

        [ForeignKey("VillaId")]
        public Villa? Villa { get; set; }

        public string? EspecialDetail { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set;}
    }
}
