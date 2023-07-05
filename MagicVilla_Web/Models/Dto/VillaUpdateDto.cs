using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        public string? Detail { get; set; }
        
        [Required]
        public double Rate { get; set; }
        
        [Required]
        public int Occupants { get; set; }
        
        [Required]
        public int SquareMeter { get; set; }
        
        [Required]
        public string? UrlImage { get; set; }
        
        public string? Amenity { get; set; }
    }
}
