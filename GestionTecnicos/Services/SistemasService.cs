using System.Linq.Expressions;
using GestionTecnicos.DAL;
using GestionTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTecnicos.Services;

public class SistemasService (IDbContextFactory<Contexto> DbFactory)
{
    /// <summary>
    /// Permite guardar un sistema, ya sea modificandolo o insertandolo, si el sistema no existe lo inserta
    /// En cambio si existe lo modifica
    /// </summary>
    /// <param name="Sistema">Sistema a ser guardado</param>
    /// <returns>Retorna un metodo insertar o Modificar</returns>
    public async Task<bool> Guardar(Sistemas Sistema)
    {
        if (!await Existe(Sistema.SistemaId))
        {
            return await Insertar(Sistema);
        }
        else
        {
            return await Modificar(Sistema);
        }
    }
    
    /// <summary>
    /// Metodo que permite verificar si existe un sistema
    /// </summary>
    /// <param name="SistemaId">Id que permite realizar la busqueda</param>
    /// <returns>Un valor booleano que indica si existe un sistema</returns>
    public async Task<bool> Existe(int SistemaId)
    {
        await using var Contexto = DbFactory.CreateDbContext();
        return await Contexto.Sistemas.AsNoTracking().AnyAsync(s => s.SistemaId == SistemaId);
    }
/// <summary>
/// Inserta un sistema en la base de datos
/// </summary>
/// <param name="Sistema">Una instancia de sistema</param>
/// <returns>un valor booleano que indica si fue añadido exitosamente</returns>
    public async Task<bool> Insertar(Sistemas Sistema)
    {
        await using var Contexto =  await DbFactory.CreateDbContextAsync();
        Contexto.Sistemas.Add(Sistema);
        return await Contexto.SaveChangesAsync() > 0;
    }
/// <summary>
/// Modifica un sistema
/// </summary>
/// <param name="Sistema">Una instancia de sistema la cual va a modificar</param>
/// <returns>Un valor booleano que indica si el sistema fue modificado exitosamente</returns>
    public async Task<bool> Modificar(Sistemas Sistema)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Sistemas.Update(Sistema);
        return await Contexto.SaveChangesAsync() > 0;
    }
/// <summary>
/// Busca una instancia de sistema por su Id
/// </summary>
/// <param name="SistemaId">Id del sistema a buscar</param>
/// <returns>Un valor booleano que indica si el sistema fue encontrado</returns>
    public async Task<Sistemas?> Buscar(int SistemaId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Sistemas.AsNoTracking().FirstOrDefaultAsync(s => s.SistemaId == SistemaId);
    }
/// <summary>
/// Elimina un sistema
/// </summary>
/// <param name="SistemaId">Id del sistema a eliminar</param>
/// <returns>un valor booleano que indica si fue eliminado exitosamente</returns>
    public async Task<bool> Eliminar(int SistemaId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Sistemas.AsNoTracking().Where(s => s.SistemaId == SistemaId).ExecuteDeleteAsync() > 0;
    }
/// <summary>
/// Busca sistemas segun un criterio especifico
/// </summary>
/// <param name="criterio">Un Criterio de búsqueda (Id,Complejidad)</param>
/// <returns>Una lista de empleados que coinciden con el criterio</returns>
    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Sistemas.Where(criterio).AsNoTracking().ToListAsync();
    }
}