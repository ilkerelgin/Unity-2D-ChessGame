using Microsoft.EntityFrameworkCore;
using Unity_2D_ChessGame_Backend.Models.Entities;

namespace Unity_2D_ChessGame_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet'ler
        public DbSet<User> Users => Set<User>();
        public DbSet<UserStatistics> UserStatistics => Set<UserStatistics>();
        public DbSet<Skin> Skins => Set<Skin>();
        public DbSet<UserSkin> UserSkins => Set<UserSkin>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<EloHistory> EloHistories => Set<EloHistory>();
        public DbSet<Friendship> Friendships => Set<Friendship>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // USER
            // ==========================================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            // ==========================================
            // USER STATISTICS (1:1 ilişki)
            // ==========================================
            modelBuilder.Entity<UserStatistics>(entity =>
            {
                entity.HasIndex(us => us.UserId).IsUnique();

                entity.HasOne(us => us.User)
                      .WithOne(u => u.Statistics)
                      .HasForeignKey<UserStatistics>(us => us.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ==========================================
            // USER SKIN (Composite Primary Key)
            // ==========================================
            modelBuilder.Entity<UserSkin>(entity =>
            {
                entity.HasKey(us => new { us.UserId, us.SkinId });

                entity.HasOne(us => us.User)
                      .WithMany(u => u.UserSkins)
                      .HasForeignKey(us => us.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(us => us.Skin)
                      .WithMany(s => s.UserSkins)
                      .HasForeignKey(us => us.SkinId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ==========================================
            // MATCH (3 FK → User, cascade sorununu önlemek için Restrict)
            // ==========================================
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasOne(m => m.WhitePlayer)
                      .WithMany(u => u.MatchesAsWhite)
                      .HasForeignKey(m => m.WhitePlayerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.BlackPlayer)
                      .WithMany(u => u.MatchesAsBlack)
                      .HasForeignKey(m => m.BlackPlayerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Winner)
                      .WithMany(u => u.MatchesWon)
                      .HasForeignKey(m => m.WinnerId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // ELO HISTORY
            // ==========================================
            modelBuilder.Entity<EloHistory>(entity =>
            {
                entity.HasOne(e => e.User)
                      .WithMany(u => u.EloHistories)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Match)
                      .WithMany(m => m.EloHistories)
                      .HasForeignKey(e => e.MatchId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.UserId, e.RecordedAt });
            });

            // ==========================================
            // FRIENDSHIP (Self-referencing, cascade sorununu önlemek için Restrict)
            // ==========================================
            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.HasIndex(f => new { f.SenderId, f.ReceiverId }).IsUnique();

                entity.HasOne(f => f.Sender)
                      .WithMany(u => u.SentFriendRequests)
                      .HasForeignKey(f => f.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Receiver)
                      .WithMany(u => u.ReceivedFriendRequests)
                      .HasForeignKey(f => f.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Kendine arkadaşlık isteği gönderemez
                entity.ToTable(t => t.HasCheckConstraint("CK_Friendship_NotSelf", "\"SenderId\" <> \"ReceiverId\""));
            });

            // ==========================================
            // NOTIFICATION
            // ==========================================
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(n => new { n.UserId, n.IsRead });
            });

            // ==========================================
            // CHAT MESSAGE
            // ==========================================
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasOne(c => c.Match)
                      .WithMany(m => m.ChatMessages)
                      .HasForeignKey(c => c.MatchId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Sender)
                      .WithMany(u => u.ChatMessages)
                      .HasForeignKey(c => c.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // REFRESH TOKEN
            // ==========================================
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(r => r.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => r.Token).IsUnique();
            });

            // Enum'ları string olarak kaydet (PostgreSQL'de okunabilirlik için)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.IsEnum)
                    {
                        property.SetProviderClrType(typeof(string));
                    }
                }
            }
        }
    }
}
