using GestionTecnicos.DAL;
using Microsoft.EntityFrameworkCore;
using GestionTecnicos.Models;
using System.Linq.Expressions;

namespace GestionTecnicos.Services;

public class TicketsService (IDbContextFactory<Contexto> DbFactory)
{

    public async Task<bool> Guardar(Tickets Ticket)
    {
        if (!await Existe(Ticket.TicketId))
        {
            return await Insertar(Ticket);
        }
        else
            return await Modificar(Ticket);
    }
    
    public async Task<bool> Existe(int TicketId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tickets.AnyAsync(t=>t.TicketId==TicketId);
    }

    public async Task<bool> Insertar(Tickets Ticket)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Add(Ticket);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tickets Ticket)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Update(Ticket);
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<Tickets?> Buscar(int TicketId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tickets.FirstOrDefaultAsync(t => t.TicketId == TicketId);
    }

    public async Task<List<Tickets?>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tickets.Where(criterio).AsNoTracking().Include(t=>t.Cliente).Include(t=>t.Cliente.Tecnico).ToListAsync();
    }

    public async Task<bool> Eliminar(int TicketId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Tickets.AsNoTracking().Where(t => t.TicketId == TicketId).ExecuteDeleteAsync() > 0;
    }
}