using EFCoreRelationships.Enums;

namespace EFCoreRelationships.DTOs
{
    public class PostCharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public RpgClass RpgClass { get; set; } = RpgClass.KNIGHT;
        public int UserId { get; set; }
    }
}
