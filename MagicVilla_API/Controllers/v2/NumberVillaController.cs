using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NumberVillaController : ControllerBase
    {
        private readonly ILogger<NumberVillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly INumberVillaRepository _numberRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumberVillaController(ILogger<NumberVillaController> logger, IVillaRepository villaRepo, INumberVillaRepository numberRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numberRepo = numberRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }        
    }
}
