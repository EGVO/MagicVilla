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

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con el Id: {0}", id);
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = _db.Villas.FirstOrDefault(v => v.Id.Equals(id));
            
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower().Equals(villaDto.Name.ToLower())) != null)
            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower().Equals(villaDto.Name.ToLower())) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese Nombre ya existe");
                return BadRequest(ModelState);
            }
            if (villaDto.Equals(null))
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDto);

            Villa model = new()
            {
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                SquareMeter = villaDto.SquareMeter,
                Rate = villaDto.Rate,
                Amenity = villaDto.Amenity
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = _db.Villas.FirstOrDefault(v => v.Id.Equals(id));
            if (villa == null)
            {
                return NotFound();
            }
            //VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            //villa.Name = villaDto.Name;
            //villa.Occupants = villaDto.Occupants;
            //villa.SquareMeter = villaDto.SquareMeter;

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                SquareMeter = villaDto.SquareMeter,
                Rate = villaDto.Rate,
                Amenity = villaDto.Amenity
            };
            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
        
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto) 
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id.Equals(id));
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id.Equals(id));

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Detail = villa.Detail,
                UrlImage = villa.UrlImage,
                Occupants = villa.Occupants,
                SquareMeter = villa.SquareMeter,
                Rate = villa.Rate,
                Amenity = villa.Amenity
            };

            if (villa == null) return BadRequest();            
           
            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                SquareMeter = villaDto.SquareMeter,
                Rate = villaDto.Rate,
                Amenity = villaDto.Amenity
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
