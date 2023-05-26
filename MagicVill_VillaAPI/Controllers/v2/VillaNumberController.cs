using AutoMapper;
using MagicVill_VillaAPI.Logging;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVill_VillaAPI.Controllers.v2
{

    [Route("api/v{version:apiVersion}/villarNumber")]
    [ApiController]
    [ApiVersion("2.0")]
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

        //[MapToApiVersion("2.0")]
        [HttpGet("GetString") ]
        public IEnumerable<string> Get()
        {
            return new string[] { "Bhrugen", "DotNetMastery" };
        }



    }
}
