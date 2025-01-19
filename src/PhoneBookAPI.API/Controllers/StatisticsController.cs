using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Application.DTOs;
using PhoneBookAPI.Application.Services;

namespace PhoneBookAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IPersonRepository _personRepository;

        public StatisticsController(IPersonService personService, IPersonRepository personRepository)
        {
            _personService = personService;
            _personRepository = personRepository;
        }

        [HttpGet("location")]
        public async Task<ActionResult<IEnumerable<LocationStatisticsDto>>> GetLocationStatistics()
        {
            var locationStats = await _personRepository.GetLocationStatisticsAsync();

            var result = locationStats.Select(s => new LocationStatisticsDto
            {
                Location = s.Key,
                PersonCount = s.Value.PersonCount,
                PhoneNumberCount = s.Value.PhoneNumberCount
            })
            .OrderByDescending(s => s.PersonCount)
            .ToList();

            return Ok(result);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<LocationStatisticsDto>> GetLocationStatistics(string location)
        {
            var locationStats = await _personRepository.GetLocationStatisticsAsync(location);

            var result = new LocationStatisticsDto
            {
                Location = location,
                PersonCount = s.Value.PersonCount,
                PhoneNumberCount = s.Value.PhoneNumberCount
            })
            .OrderByDescending(s => s.PersonCount)
            .ToList();

            return Ok(result);
        }
    }
}