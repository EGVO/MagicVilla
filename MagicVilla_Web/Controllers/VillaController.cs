using AutoMapper;
using MagicVilla_utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> villaList = new();

            var response = await _villaService.GetAll<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccessful)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        public async Task<IActionResult> CreateVilla() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid) 
            {
                var response = await _villaService.Create<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken)); 

                if (response != null) 
                {
                    TempData["successful"] = "Villa creada exitosamente...";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.Get<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            
            if (response != null && response.IsSuccessful) 
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));

                return View(_mapper.Map<VillaUpdateDto>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.Update<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.IsSuccessful)
                {
                    TempData["successful"] = "Villa actualizada exitosamente...";
                    return RedirectToAction(nameof(IndexVilla));
                } 
            }

            return View(model);
        }
        
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.Get<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            
            if (response != null && response.IsSuccessful) 
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {
            var response = await _villaService.Delete<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccessful)
            {
                TempData["successful"] = "Villa eliminada exitosamente...";
                return RedirectToAction(nameof(IndexVilla));
            } 
            
            TempData["error"] = "Ocurrio un error al eliminar.";
            return View(model);
        }
    }
}