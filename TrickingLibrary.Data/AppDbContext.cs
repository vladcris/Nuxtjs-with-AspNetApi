using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Models;

namespace TrickingLibrary.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    { }

    public DbSet<Trick>? Tricks { get; set; }
    public DbSet<Submission>? Submissions { get; set; }
}
