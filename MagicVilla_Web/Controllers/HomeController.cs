﻿using AutoMapper;
using MagicVilla_utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModel;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IMapper mapper)
        {
            _logger = logger;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber)
        {
            List<VillaDto> villaList = new();
            VillaPagedViewModel villaVM = new VillaPagedViewModel();

            if (pageNumber < 1) pageNumber = 1;

            var response = await _villaService.GetAllPaged<APIResponse>(HttpContext.Session.GetString(SD.SessionToken), pageNumber);

            if (response != null && response.IsSuccessful) 
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
                villaVM = new VillaPagedViewModel()
                {
                    VillaList = villaList,
                    PageNumber = pageNumber,
                    TotalPages = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPages))
                };

                if (pageNumber > 1) villaVM.Previous = "";
                if (villaVM.TotalPages <= pageNumber) villaVM.Next = "disabled";
            }

            return View(villaVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}