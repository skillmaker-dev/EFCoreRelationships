using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
