using Microsoft.EntityFrameworkCore;
using Williams_AP1_P1.Models;

namespace Williams_AP1_P1.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Autor> Autores => Set<Autor>();
}