using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using Williams_AP1_P1.Context;
using Williams_AP1_P1.Models;

namespace Williams_AP1_P1.Services;

public class AutorService(
    IDbContextFactory<ApplicationDbContext> contextFactory)
    : IServiceResult<Autor, int>
{
    public async Task<Result> Guardar(Autor autor)
    {
        try
        {
            bool guardado;

            if (autor.IdAutor == 0)
            {
                guardado = await Insertar(autor);
            }
            else
            {
                if (!await Existe(autor.IdAutor))
                {
                    return Result.Failure(
                        new Error("NOT_FOUND", "El autor no existe."));
                }

                guardado = await Modificar(autor);
            }

            return guardado
                ? Result.Success()
                : Result.Failure(
                    new Error("SAVE_ERROR", "No se pudo guardar el autor."));
        }
        catch (Exception)
        {
            return Result.Failure(
                new Error("SAVE_ERROR", "Ocurrio un error al guardar el autor."));
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

    public async Task<Result<Autor?>> Buscar(int autorId)
    {
        try
        {
            await using var contexto =
                await contextFactory.CreateDbContextAsync();

            var autor = await contexto.Autores
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdAutor == autorId);

            if (autor is null)
            {
                return Result.Failure<Autor?>(
                    new Error("NOT_FOUND", "No se encontro el autor."));
            }

            return Result.Success<Autor?>(autor);
        }
        catch (Exception)
        {
            return Result.Failure<Autor?>(
                new Error("SEARCH_ERROR", "Ocurrio un error al buscar el autor."));
        }
    }

    public async Task<Result<List<Autor>>> GetList(
        Expression<Func<Autor, bool>> criterio)
    {
        try
        {
            await using var contexto =
                await contextFactory.CreateDbContextAsync();

            var autores = await contexto.Autores
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();

            return Result.Success(autores);
        }
        catch (Exception)
        {
            return Result.Failure<List<Autor>>(
                new Error("LIST_ERROR", "Ocurrio un error al cargar los autores."));
        }
    }

    public async Task<Result> Eliminar(int autorId)
    {
        try
        {
            await using var contexto =
                await contextFactory.CreateDbContextAsync();

            var eliminados = await contexto.Autores
                .Where(a => a.IdAutor == autorId)
                .ExecuteDeleteAsync();

            if (eliminados == 0)
            {
                return Result.Failure(
                    new Error("NOT_FOUND", "No se encontro el autor."));
            }

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(
                new Error("DELETE_ERROR", "Ocurrio un error al eliminar el autor."));
        }
    }
}
