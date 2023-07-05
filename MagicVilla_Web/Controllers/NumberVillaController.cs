using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModel;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace MagicVilla_Web.Controllers
{
    public class NumberVillaController : Controller
    {
        private readonly INumberVillaService _numberVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumberVillaController(INumberVillaService numberVillaService, IVillaService villaService, IMapper mapper) 
        {
            _numberVillaService = numberVillaService;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexNumberVilla()
        {
            List<NumberVillaDto> numberVillaList = new();

            var response = await _numberVillaService.GetAll<APIResponse>();

            if (response != null && response.IsSuccessful) 
            {
                numberVillaList = JsonConvert.DeserializeObject<List<NumberVillaDto>>(Convert.ToString(response.Result));
            }

            return View(numberVillaList);
        }

        public async Task<IActionResult> CreateNumberVilla()
        {
            NumberVillaViewModel numberVillaVM = new();
            var response = await _villaService.GetAll<APIResponse>();

            if(response != null && response.IsSuccessful)
            {
                numberVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.Name,
                                              Value = v.Id.ToString()
                                          });
            }             

            return View(numberVillaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNumberVilla(NumberVillaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numberVillaService.Create<APIResponse>(model.NumberVilla);
                if (response != null && response.IsSuccessful)
                {
                    TempData["successful"] = "Número de Villa creada exitosamente...";
                    return RedirectToAction(nameof(IndexNumberVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            
            var res = await _villaService.GetAll<APIResponse>();

            if (res != null && res.IsSuccessful)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Result))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.Name,
                                              Value = v.Id.ToString()
                                          });
            }

            return View(model);            
        }

        public async Task<IActionResult> UpdateNumberVilla(int villaNo)
        {
            NumberVillaUpdateViewModel numberVillaVM = new();

            var response = await _numberVillaService.Get<APIResponse>(villaNo);
            if (response != null && response.IsSuccessful)
            {
                NumberVillaDto model = JsonConvert.DeserializeObject<NumberVillaDto>(Convert.ToString(response.Result));
                numberVillaVM.NumberVilla = _mapper.Map<NumberVillaUpdateDto>(model);
            }

            response = await _villaService.GetAll<APIResponse>();

            if (response != null && response.IsSuccessful)
            {
                numberVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.Name,
                                              Value = v.Id.ToString()
                                          });

                return View(numberVillaVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNumberVilla(NumberVillaUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numberVillaService.Update<APIResponse>(model.NumberVilla);
                if (response != null && response.IsSuccessful)
                {
                    TempData["successful"] = "Número de Villa actualizada exitosamente...";
                    return RedirectToAction(nameof(IndexNumberVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.GetAll<APIResponse>();

            if (res != null && res.IsSuccessful)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Result))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.Name,
                                              Value = v.Id.ToString()
                                          });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteNumberVilla(int villaNo)
        {
            NumberVillaDeleteViewModel numberVillaVM = new();

            var response = await _numberVillaService.Get<APIResponse>(villaNo);
            if (response != null && response.IsSuccessful)
            {
                NumberVillaDto model = JsonConvert.DeserializeObject<NumberVillaDto>(Convert.ToString(response.Result));
                numberVillaVM.NumberVilla = model;
            }

            response = await _villaService.GetAll<APIResponse>();

            if (response != null && response.IsSuccessful)
            {
                numberVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.Name,
                                              Value = v.Id.ToString()
                                          });

                return View(numberVillaVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNumberVilla(NumberVillaDeleteViewModel model)
        {
            var response = await _numberVillaService.Delete<APIResponse>(model.NumberVilla.VillaNo);
            if (response != null && response.IsSuccessful) 
            {
                TempData["successful"] = "Número de Villa eliminada exitosamente...";
                return RedirectToAction(nameof(IndexNumberVilla));
            }

            TempData["error"] = "Ocurrio un error al eliminar.";
            return View(model);
        }
    }
}
