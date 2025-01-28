using System.Linq.Expressions;
using GestionTecnicos.Models;
using GestionTecnicos.DAL;
using Microsoft.EntityFrameworkCore;

namespace GestionTecnicos.Services;



public class CiudadesService(IDbContextFactory<Contexto> DbFactory)

{

    public async Task<bool> Guardar(Ciudades Ciudad)
    {
        if (!await Existe(Ciudad.CiudadId))
        {
            return await Insertar(Ciudad);
        }
        else
        {
           return await Modificar(Ciudad);
        }
    }
    
    public async Task<bool> Insertar(Ciudades Ciudad)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Ciudades.Add(Ciudad);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Ciudades Ciudad)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Ciudades.Update(Ciudad);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<Ciudades?> Buscar(int ciudadId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Ciudades.FirstOrDefaultAsync(c => c.CiudadId == ciudadId);
    }

    public async Task<bool> Eliminar(int ciudadId)

    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Ciudades.AsNoTracking().Where(c => c.CiudadId == ciudadId).ExecuteDeleteAsync() >0;
    }

    public async Task<bool> Existe(int ciudadId)
    {
        await using var Contexto = DbFactory.CreateDbContext();
        return await Contexto.Ciudades.AnyAsync(c => c.CiudadId == ciudadId);
    }

    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> Criterio)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Ciudades.AsNoTracking().Where(Criterio).ToListAsync();
    }
    
    
}
    
