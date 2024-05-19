using FitnessAppAPI.Models;
using FitnessAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMembershipsController : ControllerBase
    {
        private readonly UserMembershipsService _userMembershipsService;

        public UserMembershipsController(UserMembershipsService userMembershipsService)
        {
            _userMembershipsService = userMembershipsService;
        }

        [HttpGet]
        public async Task<List<UserMemberships>> GetAll() => await _userMembershipsService.GetAllEntries();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserMemberships>> GetById(string id)
        {
            var userMemberships = await _userMembershipsService.GetEntryById(id);
            if (userMemberships == null)
            {
                return NotFound();
            }
            return userMemberships;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserMemberships newUserMemberships)
        {
            await _userMembershipsService.CreateEntry(newUserMemberships);
            return CreatedAtAction(nameof(GetById), new { id = newUserMemberships._id }, newUserMemberships);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, UserMemberships updatedUserMemberships)
        {
            var userMemberships = await _userMembershipsService.GetEntryById(id);
            if (userMemberships == null)
            {
                return NotFound();
            }
            updatedUserMemberships._id = userMemberships._id;
            await _userMembershipsService.UpdateEntry(id, updatedUserMemberships);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userMemberships = await _userMembershipsService.GetEntryById(id);
            if (userMemberships == null)
            {
                return NotFound();
            }
            await _userMembershipsService.RemoveEntry(id);
            return NoContent();
        }
    }
}
