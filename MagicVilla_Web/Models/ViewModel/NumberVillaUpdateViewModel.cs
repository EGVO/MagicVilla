using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class NumberVillaUpdateViewModel
    {
        public NumberVillaUpdateViewModel()
        {
            NumberVilla = new NumberVillaUpdateDto();
        }

        public NumberVillaUpdateDto NumberVilla { get; set; }

        public IEnumerable<SelectListItem>  VillaList { get; set; }

    }
}
