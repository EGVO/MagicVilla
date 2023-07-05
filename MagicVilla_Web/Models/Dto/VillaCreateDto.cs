using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaCreateDto
    {
        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(30)]
        public required string Name { get; set; }

        public string? Detail { get; set; }
        
        [Required(ErrorMessage = "Tarifa es Requerida")]
        public double Rate { get; set; }
        
        public int Occupants { get; set; }
        
        public int SquareMeter { get; set; }
        
        public string? UrlImage { get; set; }
        
        public string? Amenity { get; set; }
    }
}
