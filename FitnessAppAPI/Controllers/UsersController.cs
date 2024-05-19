using FitnessAppAPI.Models;
using FitnessAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
    {
        private readonly UserService _fitnessAppService;

        public UsersController(UserService fitnessAppService)
        {
               _fitnessAppService = fitnessAppService;
        }

    [HttpGet]
    public async Task<List<Users>> GetAll() => await _fitnessAppService.GetAllEntries();

    // api/users/{id}
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Users>> GetById(string id)
    {
        var user = await _fitnessAppService.GetEntryById(id);

        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    // api/users
    [HttpPost]
    public async Task<IActionResult> Post(Users newUser)
    {
        await _fitnessAppService.CreateEntry(newUser);
        return CreatedAtAction(nameof(GetById), new { id = newUser._id }, newUser);
    }

    // api/users/{id}
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string id, Users updatedUser)
    {
        var user = await _fitnessAppService.GetEntryById(id);

        if (user is null)
        {
            return NotFound();
        }
        updatedUser._id = user._id;
        await _fitnessAppService.UpdateEntry(id, updatedUser);

        return NoContent();
    }

    // api/users/{id}
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _fitnessAppService.GetEntryById(id);

        if (user is null)
        {
            return NotFound();
        }
        await _fitnessAppService.RemoveEntry(id);
        return NoContent();
    }
}

