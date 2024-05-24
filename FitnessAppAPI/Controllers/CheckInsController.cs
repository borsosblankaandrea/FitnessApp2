using FitnessAppAPI.Models;
using FitnessAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInsController : ControllerBase
    {
        private readonly CheckInsService _checkInsService;
        private readonly UserMembershipsService _userMembershipsService;

        public CheckInsController(CheckInsService checkInsService, UserMembershipsService userMembershipsService)
        {
            _checkInsService = checkInsService;
            _userMembershipsService = userMembershipsService;
        }

        [HttpGet]
        public async Task<List<CheckIns>> GetAll() => await _checkInsService.GetAllEntries();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CheckIns>> GetById(string id)
        {
            var checkIn = await _checkInsService.GetEntryById(id);
            if (checkIn == null)
            {
                return NotFound();
            }
            return checkIn;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CheckIns newCheckIn)
        {
            await _checkInsService.CreateEntry(newCheckIn);
            var userMembership = await _userMembershipsService.GetEntryById(newCheckIn.user_membership_id);

            if (userMembership != null)
            {
                userMembership.checkin_count++;
                await _userMembershipsService.UpdateEntry(userMembership._id, userMembership);
            }

            return CreatedAtAction(nameof(GetById), new { id = newCheckIn._id }, newCheckIn);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, CheckIns updatedCheckIn)
        {
            var checkIn = await _checkInsService.GetEntryById(id);
            if (checkIn == null)
            {
                return NotFound();
            }
            updatedCheckIn._id = checkIn._id;
            await _checkInsService.UpdateEntry(id, updatedCheckIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var checkIn = await _checkInsService.GetEntryById(id);
            if (checkIn == null)
            {
                return NotFound();
            }
            await _checkInsService.RemoveEntry(id);
            return NoContent();
        }
    }
}
