using AutoMapper;
using MagicVill_web.Models;
using MagicVill_web.Models.ViewModel;
using MagicVill_web.Services;
using MagicVill_web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVill_web.Controllers
{
    public class VillaNumberController : Controller
    {

        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;

        public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }


        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new List<VillaNumberDTO>();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }



        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVm villaNumberVm = new VillaNumberCreateVm();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucess)
            {
                villaNumberVm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i=> new SelectListItem { Text =i.Name,Value = i.Id.ToString() });

            }
            return View(villaNumberVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVm model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSucess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var responseList = await _villaService.GetAllAsync<APIResponse>();
            if (responseList != null && responseList.IsSucess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(responseList.Result)).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            }

            return View(model);
        }

        public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
        {
            VillaNumberUpdateVm villaNumberVm = new VillaNumberUpdateVm();
            var response = await _villaNumberService.GetAsync<APIResponse>(VillaNo);
            if (response != null && response.IsSucess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVm.VillaNumber =  _mapper.Map<VillaNumberUpdateDTO>(model);
            } 
            else
            {
                if(response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessages",response.ErrorMessages.FirstOrDefault());
                }
            }

            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucess)
            {
                villaNumberVm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            }
            return View(villaNumberVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSucess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var responseList = await _villaService.GetAllAsync<APIResponse>();
            if (responseList != null && responseList.IsSucess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(responseList.Result)).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            }

            return View(model);
        }



        public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
        {
            VillaNumberDeleteVm villaNumberVm = new VillaNumberDeleteVm();
            var response = await _villaNumberService.GetAsync<APIResponse>(VillaNo);
            if (response != null && response.IsSucess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVm.VillaNumber = model;
            }
            else
            {
                if (response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }
            }

            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucess)
            {
                villaNumberVm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            }
            return View(villaNumberVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo);
                if (response != null && response.IsSucess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
               
            }
           
            return View(model);
        }

    }
}
