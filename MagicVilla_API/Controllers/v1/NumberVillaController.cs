using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumberVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Números de Villas");

                IEnumerable<NumberVilla> numberVillaList = await _numberRepo.GetAll(IncludeProperties: "Villa");

                _response.Result = _mapper.Map<IEnumerable<NumberVillaDto>>(numberVillaList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetNumberVilla")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Villa con el Id: {0}", id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;

                    return BadRequest(_response);
                }

                var numberVilla = await _numberRepo.Get(v => v.VillaNo.Equals(id), IncludeProperties: "Villa");

                if (numberVilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful = false;

                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<NumberVillaDto>(numberVilla);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumberVilla([FromBody] NumberVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numberRepo.Get(v => v.VillaNo.Equals(createDto.VillaNo)) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El número de Villa ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Id.Equals(createDto.VillaId)) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Id de la Villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDto.Equals(null))
                {
                    return BadRequest(createDto);
                }

                NumberVilla model = _mapper.Map<NumberVilla>(createDto);

                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                await _numberRepo.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumberVilla", new { id = model.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                var numberVilla = await _numberRepo.Get(v => v.VillaNo.Equals(id));
                if (numberVilla == null)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

                await _numberRepo.Delete(numberVilla);

                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumberVilla(int id, [FromBody] NumberVillaUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                if (await _villaRepo.Get(v => v.Id.Equals(updateDto.VillaId)) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Id de la Villa no existe");
                    return BadRequest(ModelState);
                }

                NumberVilla model = _mapper.Map<NumberVilla>(updateDto);

                await _numberRepo.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return BadRequest(_response);
        }
    }
}
