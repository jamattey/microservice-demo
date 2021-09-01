using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        [HttpGet ("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlaformById (int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult <PlatformReadDto> CreatePlatform  (PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlaformById), new {Id = PlatformReadDto.Id}, PlatformReadDto);
        }

        

        



    }
}