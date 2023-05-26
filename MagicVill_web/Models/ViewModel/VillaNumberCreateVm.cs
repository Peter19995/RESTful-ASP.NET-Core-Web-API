using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVill_web.Models.ViewModel
{
    public class VillaNumberUpdateVm
    { 
        public VillaNumberUpdateVm()
        {
            VillaNumber  = new VillaNumberUpdateDTO();   
        }
        public VillaNumberUpdateDTO VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
