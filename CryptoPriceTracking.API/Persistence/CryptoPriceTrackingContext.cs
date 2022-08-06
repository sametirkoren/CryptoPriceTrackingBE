using Microsoft.EntityFrameworkCore;

namespace CryptoPriceTracking.API.Persistence;

public class CryptoPriceTrackingContext : DbContext
{
    public CryptoPriceTrackingContext(DbContextOptions<CryptoPriceTrackingContext> options) : base(options){}
    
    public DbSet<Crypto> Cryptos { get; set; }
}