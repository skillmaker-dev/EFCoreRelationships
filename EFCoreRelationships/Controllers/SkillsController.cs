using EFCoreRelationships.Data;
using EFCoreRelationships.DTOs;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DataContext _context;

        public SkillsController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetSkills()
        {
            var skills = await _context.Skills.ToListAsync();

            return Ok(skills);
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill(PostSkillDto skillDto)
        {
            var newSkill = new Skill
            {
                Name = skillDto.Name
            };

            _context.Skills.Add(newSkill);

            if (await _context.SaveChangesAsync() < 1)
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot add skill");

            return Ok(newSkill);
        }

        [HttpPost("add-skill-to-character")]
        public async Task<ActionResult<Character>> AddSkillToCharacter(PostSkillCharacterDto skillCharacterDto)
        {
            var character = await _context.Characters.Include(c => c.Skills).Include(c => c.Weapon).FirstOrDefaultAsync(c => c.Id == skillCharacterDto.CharacterId);

            if (character is null)
                return NotFound("Character not found");

            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillCharacterDto.SkillId);

            if (skill is null)
                return NotFound("Skill not found");

            character.Skills.Add(skill);

            if (await _context.SaveChangesAsync() < 1)
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot add skill");

            return Ok(character);
        }
    }
}
