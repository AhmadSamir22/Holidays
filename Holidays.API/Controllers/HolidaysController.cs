using AutoMapper;
using Holidays.API.DTOs;
using Holidays.DAL.Models;
using Holidays.REPO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly UnitOfWork _workUnit;
        private readonly IMapper _mapper;

        public HolidaysController(UnitOfWork workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        [HttpGet("GetHolidays")]
        public async Task<IActionResult> GetHolidays()
        {
            var holidays = await _workUnit.HolidayRepo.GetHolidays();
            var response = new
            {
                Holidays = holidays
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHolidaysByCountry(int id)
        {
            var holidays = await _workUnit.HolidayRepo.GetHolidaysByCountry(id);
            var response = new
            {
                Holidays = holidays
            };
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateHoliday(HolidayCreateDto holidayDto)
        {
            var holidayExist = _workUnit.HolidayRepo.GetHolidayByName(holidayDto.Name);
            if (holidayExist != null)
                return BadRequest(new { Message ="Holiday with given name already exists"});
            var holiday = _mapper.Map<Holiday>(holidayDto);
            holiday.CreatedAt = DateTime.Now;
            holiday.UpdatedAt = DateTime.Now;
            _workUnit.HolidayRepo.Add(holiday);
            await _workUnit.Save();
            return Ok(new { Message = "Holiday Added Successifully" });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateHoliday(HolidayUpdateDto holidayDto)
        {
            var holidayExist = _workUnit.HolidayRepo.GetHolidayById(holidayDto.ID);
            if (holidayExist == null)
                return BadRequest(new { Message = "Holiday with givern id doesn't exist" });
            var holiday = _mapper.Map<Holiday>(holidayDto);
            _workUnit.HolidayRepo.Update(holiday);
            await _workUnit.Save();
            return Ok(new { Message = "Holiday Updated Successifully" });
        }

        [HttpPost("Toggle")]
        //if Deleted reverese and vice versa
        public async Task<IActionResult> ToggleHoliday(int id)
        {
            var holidayExist = _workUnit.HolidayRepo.GetHolidayById(id);
            if (holidayExist == null)
                return BadRequest(new { Message = "Holiday with givern id doesn't exist" });
            _workUnit.HolidayRepo.Delete(id);
            await _workUnit.Save();
            return Ok(new { Message = "Toggled Successifully"});
        }

    }
}
