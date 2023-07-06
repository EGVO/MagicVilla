using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Models.ViewModel
{
    public class VillaPagedViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public string Previous { get; set; } = "disabled";

        public string Next { get; set; } = "";

        public IEnumerable<VillaDto> VillaList { get; set; }
    }
}
