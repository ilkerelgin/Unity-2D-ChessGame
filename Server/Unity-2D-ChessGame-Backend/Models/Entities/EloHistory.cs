using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class EloHistory
    {
        [Key]
        public long Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public Guid MatchId { get; set; }

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;

        public int OldElo { get; set; }

        public int NewElo { get; set; }

        public int Change { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }
}
