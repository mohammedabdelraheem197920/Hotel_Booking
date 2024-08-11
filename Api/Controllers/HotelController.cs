﻿using Application.DTOs;
using AutoMapper;
using Core.Models;
using Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IHotelRepository hotelRepository;

        public HotelController(IMapper mapper, IHotelRepository hotelRepository)
        {
            this.mapper = mapper;
            this.hotelRepository = hotelRepository;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            List<Hotel> hotelsDB = hotelRepository.GetAll().ToList();
            List<HotelForGetDto> hotelsDTO = mapper.Map<List<HotelForGetDto>>(hotelsDB);
            if (hotelsDB != null)
            {
                return Ok(hotelsDTO);
            }
            return BadRequest();

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Hotel hotelDB = hotelRepository.GetById(id);
            HotelForGetDto hotelDTO = mapper.Map<HotelForGetDto>(hotelDB);
            if (hotelDB != null)
            {
                return Ok(hotelDTO);
            }
            return BadRequest();
        }


    }

}
