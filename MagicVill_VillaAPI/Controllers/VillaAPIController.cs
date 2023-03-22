using MagicVill_VillaAPI.Data;
using MagicVill_VillaAPI.Logging;
using MagicVill_VillaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVill_VillaAPI.Controllers
{
    [Route("api/villaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger)
        {
            _logger = logger; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas", "");
            return Ok(VillaStore.villaList);
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
            var villac = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
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
            if(VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villa.Name.ToLower())!= null)
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
            villa.Id = VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1; 
            VillaStore.villaList.Add(villa);
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
            var villac = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(villac == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villac);
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
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if(villa == null) { return NotFound(); }
            villa.Name = villaDTO.Name;
            villa.Occupancy = villaDTO.Occupancy;   
            villa.Sqft = villaDTO.Sqft;

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0) { return BadRequest(); }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null) { return BadRequest(); }
            patchDTO.ApplyTo(villa,ModelState);
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return NoContent() ;

        }


    }
}
 