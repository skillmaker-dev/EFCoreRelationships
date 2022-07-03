using EFCoreRelationships.Enums;
using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RpgClass RpgClass { get; set; } = RpgClass.KNIGHT;

        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }


        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
