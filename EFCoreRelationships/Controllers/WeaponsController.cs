using EFCoreRelationships.Data;
using EFCoreRelationships.DTOs;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        private readonly DataContext _context;

        public WeaponsController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Weapon>>> GetWeapons()
        {
            var weapons = await _context.Weapons.ToListAsync();

            return Ok(weapons);
        }

        [HttpPost]
        public async Task<ActionResult<Weapon>> AddWeapon(PostWeaponDto weaponDto)
        { 
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == weaponDto.CharacterId);
            if (character is null)
                return NotFound("Character not found");

            var newWeapon = new Weapon
            {
                CharacterId = weaponDto.CharacterId,
                Name = weaponDto.Name,
                Damage = weaponDto.Damage,
                Character = character
            };
            _context.Weapons.Add(newWeapon);

            if (await _context.SaveChangesAsync() < 1)
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot add weapon");

            return Ok(newWeapon);
        }
    }
}
