using System.ComponentModel.DataAnnotations;
using Unity_2D_ChessGame_Backend.Models.Enums;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class Skin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public SkinType Type { get; set; }

        [Required]
        [MaxLength(256)]
        public string AssetBundleKey { get; set; } = string.Empty;

        [MaxLength(512)]
        public string? ThumbnailUrl { get; set; }

        public int Price { get; set; }

        public SkinRarity Rarity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<UserSkin> UserSkins { get; set; } = new List<UserSkin>();
    }
}
