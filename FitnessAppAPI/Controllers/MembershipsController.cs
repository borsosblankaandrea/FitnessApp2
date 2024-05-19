using FitnessAppAPI.Models;
using FitnessAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsController : ControllerBase
    {
        private readonly MembershipsService _membershipService;

        public MembershipsController(MembershipsService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public async Task<List<Memberships>> GetAll() => await _membershipService.GetAllMemberships();

        // api/memberships/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Memberships>> GetById(string id)
        {
            var membership = await _membershipService.GetMembershipById(id);

            if (membership == null)
            {
                return NotFound();
            }
            return membership;
        }

        // api/memberships
        [HttpPost]
        public async Task<IActionResult> Post(Memberships newMembership)
        {
            await _membershipService.CreateMembership(newMembership);
            return CreatedAtAction(nameof(GetById), new { id = newMembership._id }, newMembership);
        }

        // api/memberships/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Memberships updatedMembership)
        {
            var membership = await _membershipService.GetMembershipById(id);

            if (membership is null)
            {
                return NotFound();
            }
            updatedMembership._id = membership._id;
            await _membershipService.UpdateMembership(id, updatedMembership);

            return NoContent();
        }

        // api/memberships/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var membership = await _membershipService.GetMembershipById(id);

            if (membership is null)
            {
                return NotFound();
            }
            await _membershipService.RemoveMembership(id);
            return NoContent();
        }
    }
}
