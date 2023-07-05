using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class NumberVillaDeleteViewModel
    {
        public NumberVillaDeleteViewModel()
        {
            NumberVilla = new NumberVillaDto();
        }

        public NumberVillaDto NumberVilla { get; set; }

        public IEnumerable<SelectListItem>  VillaList { get; set; }

    }
}
