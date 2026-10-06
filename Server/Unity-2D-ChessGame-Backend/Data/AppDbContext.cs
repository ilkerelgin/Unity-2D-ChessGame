using Microsoft.EntityFrameworkCore;




namespace Unity_2D_ChessGame_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


    }
}
