using MagicVill_VillaAPI.Data;
using MagicVill_VillaAPI.Logging;
using MagicVill_VillaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVill_VillaAPI.Controllers
{
    [Route("api/villaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas", "");
            return Ok(_db.Villas.ToList());
        }



        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        public ActionResult GetVillas(int id)
        {
            if(id == 0)
            {
                _logger.Log("Get Villa Error with Id " + id, "error");
                return BadRequest();
            }
            var villac = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villac == null)
            {
                return NotFound();
            }
            return Ok(villac);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult <VillaDTO> CreateVilla ([FromBody] VillaDTO villa)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            // if villa already exists return custome validation.
            if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villa.Name.ToLower())!= null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);

            }
            if(villa.Id> 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa mode = new Villa()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                ImageUrl = villa.ImageUrl,

            };
            _db.Villas.Add(mode);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla",new {id = villa.Id},villa); 
        }



        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Deletevilla (int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villac = _db.Villas.FirstOrDefault(u => u.Id == id);
            if(villac == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villac);
            _db.SaveChanges();
            return NoContent();
        }



        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO) { 

            if(villaDTO == null || id != villaDTO.Id) {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //if(villa == null) { return NotFound(); }
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;   
            //villa.Sqft = villaDTO.Sqft;

            Villa mode = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.ImageUrl,

            };
            _db.Villas.Update(mode);
            _db.SaveChanges();
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0) { return BadRequest(); }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);
            //villa.Name = "new Name";
            //_db.SaveChanges();

            if (villa == null) { return BadRequest(); }
            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                ImageUrl = villa.ImageUrl,

            };
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa mode = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.ImageUrl,

            };
            _db.Villas.Update(mode);
            _db.SaveChanges();
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return NoContent() ;

        }


    }
}
 