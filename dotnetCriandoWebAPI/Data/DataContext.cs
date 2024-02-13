using dotnetCriandoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts)
    {
    }

    public DbSet<Filme> Filme { get; set; }
    public DbSet<Cinema> Cinema { get; set; }
}