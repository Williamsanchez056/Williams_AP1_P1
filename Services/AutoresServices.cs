using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Williams_AP1_P1.Context;
using Williams_AP1_P1.Models;

namespace Williams_AP1_P1.Services;

public class AutorService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public AutorService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int idAutor)
    {
        using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.Autores
            .AnyAsync(a => a.IdAutor == idAutor);
    }

    public async Task<bool> Guardar(Autor autor)
    {
        using var contexto = await _dbFactory.CreateDbContextAsync();

        if (autor.IdAutor == 0)
        {
            contexto.Autores.Add(autor);
        }
        else
        {
            var autorExistente = await contexto.Autores
                .FindAsync(autor.IdAutor);

            if (autorExistente == null)
            {
                return false;
            }

            autorExistente.Nombre = autor.Nombre;
            autorExistente.Nacionalidad = autor.Nacionalidad;
            autorExistente.FechaNacimiento = autor.FechaNacimiento;
            autorExistente.Sueldo = autor.Sueldo;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int idAutor)
    {
        using var contexto = await _dbFactory.CreateDbContextAsync();

        var autor = await contexto.Autores.FindAsync(idAutor);

        if (autor == null)
        {
            return false;
        }

        contexto.Autores.Remove(autor);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Autor?> Buscar(int idAutor)
    {
        using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.Autores
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.IdAutor == idAutor);
    }

    public async Task<List<Autor>> Listar(
        Expression<Func<Autor, bool>> criterio)
    {
        using var contexto = await _dbFactory.CreateDbContextAsync();

        return await contexto.Autores
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
