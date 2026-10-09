using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Unity_2D_ChessGame_Backend.Models.Enums;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(512)]
        public string? AvatarUrl { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        // Aktif kostüm
        public int? ActiveSkinId { get; set; }

        [ForeignKey(nameof(ActiveSkinId))]
        public Skin? ActiveSkin { get; set; }

        public int EloRating { get; set; } = 1200;

        public int Currency { get; set; } = 0;

        public bool IsOnline { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public UserStatistics? Statistics { get; set; }
        public ICollection<UserSkin> UserSkins { get; set; } = new List<UserSkin>();
        public ICollection<Match> MatchesAsWhite { get; set; } = new List<Match>();
        public ICollection<Match> MatchesAsBlack { get; set; } = new List<Match>();
        public ICollection<Match> MatchesWon { get; set; } = new List<Match>();
        public ICollection<EloHistory> EloHistories { get; set; } = new List<EloHistory>();
        public ICollection<Friendship> SentFriendRequests { get; set; } = new List<Friendship>();
        public ICollection<Friendship> ReceivedFriendRequests { get; set; } = new List<Friendship>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
