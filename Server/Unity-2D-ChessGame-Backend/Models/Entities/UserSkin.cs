using System.ComponentModel.DataAnnotations.Schema;
using Unity_2D_ChessGame_Backend.Models.Enums;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class UserSkin
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public int SkinId { get; set; }

        [ForeignKey(nameof(SkinId))]
        public Skin Skin { get; set; } = null!;

        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;

        public AcquireMethod AcquireMethod { get; set; }
    }
}
