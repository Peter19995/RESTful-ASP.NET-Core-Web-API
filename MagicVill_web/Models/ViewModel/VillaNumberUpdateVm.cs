using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVill_web.Models.ViewModel
{
    public class VillaNumberCreateVm
    {
        public VillaNumberCreateVm()
        {
            VillaNumber  = new VillaNumberCreateDTO();   
        }
        public VillaNumberCreateDTO VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
