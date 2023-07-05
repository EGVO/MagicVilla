using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class NumberVillaViewModel
    {
        public NumberVillaViewModel()
        {
            NumberVilla = new NumberVillaCreateDto();
        }

        public NumberVillaCreateDto NumberVilla { get; set; }

        public IEnumerable<SelectListItem>  VillaList { get; set; }

    }
}
