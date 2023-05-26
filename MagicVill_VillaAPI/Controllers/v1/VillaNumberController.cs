using AutoMapper;
using MagicVill_VillaAPI.Logging;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVill_VillaAPI.Controllers.v1
{

    [Route("api/v{version:apiVersion}/villarNumber")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVilla;
        private readonly IVillaRepository _Villa;
        public VillaNumberController(ILogging logger, IVillaNumberRepository dbVilla, IMapper mapper, IVillaRepository villa)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
            _Villa = villa;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                _logger.Log("Getting all villas", "");
                IEnumerable<VillaNumber> villaNoList = await _dbVilla.GetAllAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNoList);
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

        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "String 2" };
        }

        [HttpGet("{VillaNo:int}", Name = "GetVillaNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVillas(int VillaNo)
        {
            try
            {
                if (VillaNo == 0)
                {
                    _logger.Log("Get Villa Error with Villa No " + VillaNo, "error");

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSucess = false;
                    return BadRequest(_response);
                }
                var villac = await _dbVilla.GetAsync(u => u.VillaNo == VillaNo, includeProperties: "Villa");
                if (villac == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSucess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villac);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSucess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already Exists");
                    _response.ErrorMessages = new List<string> { "Villa Number already Exists" };
                    _response.Result = ModelState;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSucess = false;
                    return BadRequest(_response);
                }
                if (await _Villa.GetAsync(u => u.Id == createDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID Is Invalid");
                    _response.ErrorMessages = new List<string> { "Villa ID Is Invalid" };
                    return BadRequest(ModelState);
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

                VillaNumber villa = _mapper.Map<VillaNumber>(createDTO);

                await _dbVilla.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSucess = true;
                return CreatedAtRoute("GetVilla", new { id = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("{VillaNo:int}", Name = "DeleteVillaNo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Deletevilla(int VillaNo)
        {
            try
            {
                if (VillaNo == 0)
                {
                    return BadRequest();
                }
                var villac = await _dbVilla.GetAsync(u => u.VillaNo == VillaNo);
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


        [HttpPut("{VillaNo:int}", Name = "UpdateVillaNo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int VillaNo, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || VillaNo != updateDTO.VillaNo)
                {
                    return BadRequest();
                }
                if (await _Villa.GetAsync(u => u.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID Is Invalid");
                    _response.ErrorMessages = new List<string> { "Villa ID Is Invalid" };
                    return BadRequest(ModelState);
                }
                VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

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


    }
}
