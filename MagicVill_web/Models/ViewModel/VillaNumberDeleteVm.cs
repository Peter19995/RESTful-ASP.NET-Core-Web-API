using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVill_web.Models.ViewModel
{
    public class VillaNumberDeleteVm
    {
        public VillaNumberDeleteVm()
        {
            VillaNumber  = new VillaNumberDTO();   
        }
        public VillaNumberDTO VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
