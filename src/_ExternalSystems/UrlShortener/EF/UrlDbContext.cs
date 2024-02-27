using Microsoft.EntityFrameworkCore;
using UrlShortener.Url;

namespace UrlShortener.EF;

internal sealed class UrlDbContext : DbContext
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>().HasKey(x => x.Id);
        modelBuilder.Entity<ShortenedUrl>().HasIndex(x => x.Code).IsUnique();
    }
}