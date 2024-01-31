using dotnetCriandoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetCriandoWebAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts)
    {
    }

    public DbSet<Filme> Filmes { get; set; }
}