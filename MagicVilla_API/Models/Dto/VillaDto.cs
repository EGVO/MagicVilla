using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        public string? Detail { get; set; }
        
        [Required]
        public double Rate { get; set; }
        
        public int Occupants { get; set; }
        
        public int SquareMeter { get; set; }
        
        public string? UrlImage { get; set; }
        
        public string? Amenity { get; set; }
    }
}
