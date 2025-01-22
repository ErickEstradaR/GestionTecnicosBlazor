using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionTecnicos.DAL;
using GestionTecnicos.Models;
using Microsoft.EntityFrameworkCore.Update;

namespace GestionTecnicos.Services;

public class TecnicosService (IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int TecnicoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
    }

    public async Task<bool> ExisteTecnicoConNombre(string nombre)
    {
     
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tecnicos
            .AnyAsync(t => t.Nombres.ToLower() ==
                           nombre.ToLower());
    }
    
    public async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var Contexto  = await DbFactory.CreateDbContextAsync();
        Contexto.Tecnicos.Add(tecnico);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int TecnicoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tecnicos.AsNoTracking().Where(t => t.TecnicoId == TecnicoId).ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Update(tecnico);
        return await Contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
        {
            return await Insertar(tecnico);
        }
        else
        {
            return await Modificar(tecnico);
        }
    }
    
    public async Task<Tecnicos?> Buscar(int TecnicoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tecnicos
            .FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);    

}

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>>criterio)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tecnicos.Where(criterio).AsNoTracking().ToListAsync();
    }
}