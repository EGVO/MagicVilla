using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas");

            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con el Id: {0}", id);
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id.Equals(id));
            
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower().Equals(villaDto.Name.ToLower())) != null)
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Name.ToLower().Equals(createDto.Name.ToLower())) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese Nombre ya existe");
                return BadRequest(ModelState);
            }
            if (createDto.Equals(null))
            {
                return BadRequest(createDto);
            }
            //if (villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDto);

            Villa model = _mapper.Map<Villa>(createDto);
            //Villa model = new()
            //{
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    UrlImage = villaDto.UrlImage,
            //    Occupants = villaDto.Occupants,
            //    SquareMeter = villaDto.SquareMeter,
            //    Rate = villaDto.Rate,
            //    Amenity = villaDto.Amenity
            //};

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id.Equals(id));
            if (villa == null)
            {
                return NotFound();
            }
            //VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) 
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            //villa.Name = villaDto.Name;
            //villa.Occupants = villaDto.Occupants;
            //villa.SquareMeter = villaDto.SquareMeter;

            Villa model = _mapper.Map<Villa>(updateDto);    
            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    UrlImage = villaDto.UrlImage,
            //    Occupants = villaDto.Occupants,
            //    SquareMeter = villaDto.SquareMeter,
            //    Rate = villaDto.Rate,
            //    Amenity = villaDto.Amenity
            //};
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto) 
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id.Equals(id));

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(patchDto);
            //VillaUpdateDto villaDto = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Detail = villa.Detail,
            //    UrlImage = villa.UrlImage,
            //    Occupants = villa.Occupants,
            //    SquareMeter = villa.SquareMeter,
            //    Rate = villa.Rate,
            //    Amenity = villa.Amenity
            //};

            if (villa == null) return BadRequest();            
           
            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(villaDto);
            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    UrlImage = villaDto.UrlImage,
            //    Occupants = villaDto.Occupants,
            //    SquareMeter = villaDto.SquareMeter,
            //    Rate = villaDto.Rate,
            //    Amenity = villaDto.Amenity
            //};

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
