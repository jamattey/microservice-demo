using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
        public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            //Assign repository and mapper to private fields to allow for dependency injection

            _repository = repository;
            _mapper = mapper;
        }

        //Test
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine ("--> Getting Platforms.....");

            var platformItem = _repository.GetAllPlatforms();

            return Ok (_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));            
        }

        [HttpGet ("{id}")]
        public ActionResult<PlatformReadDto> GetPlaformById (int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        

        



    }
}