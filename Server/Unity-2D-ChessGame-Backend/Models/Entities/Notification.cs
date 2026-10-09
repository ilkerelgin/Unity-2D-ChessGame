using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Unity_2D_ChessGame_Backend.Models.Enums;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class Notification
    {
        [Key]
        public long Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public NotificationType Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// İlgili nesne ID'si (maç ID, kullanıcı ID vb.)
        /// </summary>
        [MaxLength(256)]
        public string? RelatedEntityId { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
