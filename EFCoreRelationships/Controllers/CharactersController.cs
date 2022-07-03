using EFCoreRelationships.Data;
using EFCoreRelationships.DTOs;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly DataContext _context;

        public CharactersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharactersByUserId(int userId)
        {
            var characters = await _context.Characters.Where(c => c.UserId == userId).Include(c => c.Weapon).Include(c => c.Skills).ToListAsync();

            if(!characters.Any())
                return NotFound();

            return characters;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(PostCharacterDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user is null)
                return NotFound("User not found");

            var newCharacter = new Character
            {
                UserId = request.UserId,
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user
            };

            await _context.Characters.AddAsync(newCharacter);

            if (await _context.SaveChangesAsync() < 1)
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not save character");

            return Ok(newCharacter);

        }
    }
}
