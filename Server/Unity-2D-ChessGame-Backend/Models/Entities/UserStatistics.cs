using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class UserStatistics
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public int TotalGamesPlayed { get; set; } = 0;

        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public int Draws { get; set; } = 0;

        public int WinStreak { get; set; } = 0;

        public int BestWinStreak { get; set; } = 0;

        public int HighestElo { get; set; } = 1200;

        public int TotalPlayTimeMinutes { get; set; } = 0;
    }
}
