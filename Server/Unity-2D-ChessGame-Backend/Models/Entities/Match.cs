using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Unity_2D_ChessGame_Backend.Models.Enums;

namespace Unity_2D_ChessGame_Backend.Models.Entities
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid WhitePlayerId { get; set; }

        [ForeignKey(nameof(WhitePlayerId))]
        public User WhitePlayer { get; set; } = null!;

        public Guid BlackPlayerId { get; set; }

        [ForeignKey(nameof(BlackPlayerId))]
        public User BlackPlayer { get; set; } = null!;

        public Guid? WinnerId { get; set; }

        [ForeignKey(nameof(WinnerId))]
        public User? Winner { get; set; }

        public MatchResult Result { get; set; }

        public MatchEndReason EndReason { get; set; }

        /// <summary>
        /// Tüm hamleler PGN (Portable Game Notation) formatında
        /// </summary>
        public string Pgn { get; set; } = string.Empty;

        public int TotalMoves { get; set; }

        public int WhiteEloBeforeMatch { get; set; }

        public int BlackEloBeforeMatch { get; set; }

        public int WhiteEloChange { get; set; }

        public int BlackEloChange { get; set; }

        /// <summary>
        /// Süre kontrolü (saniye cinsinden, örn: 600 = 10 dakika)
        /// </summary>
        public int TimeControlSeconds { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        // Navigation properties
        public ICollection<EloHistory> EloHistories { get; set; } = new List<EloHistory>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
