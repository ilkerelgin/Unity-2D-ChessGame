using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Unity_2D_ChessGame_Backend.Data;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


// 1. Supabase (PostgreSQL) Bağlantısını Ayarla
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));


// 2. Redis Bağlantısını Ayarla
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!));






builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();






using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // 1. Supabase (PostgreSQL) Bağlantı Testi
        var dbContext = services.GetRequiredService<AppDbContext>();
        bool isSupabaseConnected = await dbContext.Database.CanConnectAsync();

        if (isSupabaseConnected)
            Console.WriteLine("✅ Supabase (PostgreSQL) bağlantısı BAŞARILI!");
        else
            Console.WriteLine("❌ Supabase bağlantısı KURULAMADI! Şifreyi kontrol et.");

        // 2. Redis Bağlantı Testi
        var redis = services.GetRequiredService<StackExchange.Redis.IConnectionMultiplexer>();
        var ping = await redis.GetDatabase().PingAsync();
        Console.WriteLine($"✅ Redis bağlantısı BAŞARILI! (Gecikme: {ping.TotalMilliseconds} ms)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Veritabanlarına bağlanırken bir HATA oluştu:\n{ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
