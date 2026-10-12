using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Williams_AP1_P1.Context;
using Williams_AP1_P1.Models;

namespace Williams_AP1_P1.Services;

public class AutorService(
    IDbContextFactory<ApplicationDbContext> contextFactory)
    :Aplicada1.Core.IService<Autor, int>
{
    public async Task<bool> Guardar(Autor autor)
    {
        if (!await Existe(autor.IdAutor))
        {
            return await Insertar(autor);
        }
        else
        {
            return await Modificar(autor);
        }
    }

    public async Task<bool> Existe(int autorId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Autores
            .AnyAsync(a => a.IdAutor == autorId);
    }

    public async Task<bool> Insertar(Autor autor)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Autores.Add(autor);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Autor autor)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Update(autor);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Autor?> Buscar(int autorId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Autores
            .FirstOrDefaultAsync(a => a.IdAutor == autorId);
    }

    public async Task<List<Autor>> GetList(
        Expression<Func<Autor, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Autores
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int autorId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Autores
            .Where(a => a.IdAutor == autorId)
            .ExecuteDeleteAsync() > 0;
    }
}
