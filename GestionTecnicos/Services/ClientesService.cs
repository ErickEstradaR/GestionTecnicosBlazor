using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace GestionTecnicos.Services;
using GestionTecnicos.Models;
using GestionTecnicos.DAL;

public class ClientesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Clientes Cliente)
    {
        if (!await Existe(Cliente.ClienteId))
        {
            return await Insertar(Cliente);
        }

        else
        {
            return await Modificar(Cliente);
        }
    }


    public async Task<bool> Existe(int ClienteId)
    {
        await using var Contexto = DbFactory.CreateDbContext();
        return await Contexto.Clientes.AnyAsync(c => c.ClienteId == ClienteId);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> Criterio)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Clientes.Where(Criterio).AsNoTracking().Include(c => c.Tecnico).ToListAsync();
    }

    public async Task<Clientes?> Buscar(int ClienteId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Clientes.Include(c => c.Tecnico).Include(c=>c.Ciudad).FirstOrDefaultAsync(c => c.ClienteId == ClienteId);
    }

    public async Task<bool> Eliminar(int ClienteId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Clientes.AsNoTracking().Where(c => c.ClienteId == ClienteId).ExecuteDeleteAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes Cliente)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Clientes.Update(Cliente);
        return await Contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Insertar(Clientes Cliente)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Clientes.Add(Cliente);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExisteRnc(int ClienteId, string rnc)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Clientes
            .AnyAsync(c => c.ClienteId != ClienteId
                           && c.Nombres.ToLower().Equals(rnc.ToLower()));

    }
    
    public async Task<bool> ExisteNombre(int ClienteId,string nombre)
    {
     
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Clientes
            .AnyAsync(c => c.ClienteId != ClienteId
                           && c.Nombres.ToLower().Equals(nombre.ToLower()));
    }
}