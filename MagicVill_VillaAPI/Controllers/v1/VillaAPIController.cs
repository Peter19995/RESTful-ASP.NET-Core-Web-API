using AutoMapper;
using MagicVill_VillaAPI.Data;
using MagicVill_VillaAPI.Logging;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVill_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/villaAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public VillaAPIController(ILogging logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        //caching data
        [ResponseCache(CacheProfileName = "Default30")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetVillas([FromQuery(Name = "FilterOccupancy")] int? occupancy, [FromQuery] string? search, int pageSize = 2, int PageNumber = 1)
        {
            try
            {
                _logger.Log("Getting all villas", "");

                IEnumerable<Villa> villasList;
                if (occupancy >0 )
                {
                    villasList = await _dbVilla.GetAllAsync(u => u.Occupancy == occupancy,pageSize:pageSize,PageNumber:PageNumber);
                }
                else
                {
                    villasList = await _dbVilla.GetAllAsync();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    villasList = villasList.Where(u => u.Amenity.ToLower().Contains(search.ToLower()) || u.Name.ToLower().Contains(search.ToLower()));
                }
                _response.Result = _mapper.Map<List<VillaDTO>>(villasList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSucess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVillas(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log("Get Villa Error with Id " + id, "error");

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSucess = false;
                    return BadRequest(_response);
                }
                var villac = await _dbVilla.GetAsync(u => u.Id == id);
                if (villac == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSucess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaDTO>(villac);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSucess = true; 
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already Exists");
                    _response.Result = ModelState;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSucess = false;
                    return BadRequest(_response);
                }
                if (createDTO == null)
                {
                    _response.Result = createDTO;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSucess = false;
                    return BadRequest(_response);

                }
                //if(villa.Id> 0) {
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}

                Villa villa = _mapper.Map<Villa>(createDTO);

                await _dbVilla.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSucess = true;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }



        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        //[Authorize(Roles = "CUSTOM")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> Deletevilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villac = await _dbVilla.GetAsync(u => u.Id == id);
                if (villac == null)
                {
                    return NotFound();
                }
                await _dbVilla.RemoveAsync(villac);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }



        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                Villa model = _mapper.Map<Villa>(updateDTO);

                await _dbVilla.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            try
            {
                if (patchDTO == null || id == 0) { return BadRequest(); }
                var villa = await _dbVilla.GetAsync(v => v.Id == id, track: false);
                //villa.Name = "new Name";
                //await _dbVilla.SaveChangesAsync();

                if (villa == null) { return BadRequest(); }
                VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

                patchDTO.ApplyTo(villaDTO, ModelState);
                Villa mode = _mapper.Map<Villa>(villaDTO);

                await _dbVilla.UpdateAsync(mode);
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }


    }
}
