using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository branchRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public BranchController(IBranchRepository branchRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            this.branchRepository = branchRepository;
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            string[] includeProperties = { "Hotel" };

            List<Branch> branchsDB = branchRepository.GetAll(includeProperties).ToList();
            List<BranchForGetDto> branchsDTO = mapper.Map<List<BranchForGetDto>>(branchsDB);
            if (branchsDB != null)
            {
                return Ok(branchsDTO);
            }
            return BadRequest();

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            string[] includeProperties = { "Hotel" };
            Branch branchDB = branchRepository.GetById(id, includeProperties);
            BranchForGetDto branchDTO = mapper.Map<BranchForGetDto>(branchDB);
            if (branchDB != null)
            {
                return Ok(branchDTO);
            }
            return BadRequest();
        }

    }
}
