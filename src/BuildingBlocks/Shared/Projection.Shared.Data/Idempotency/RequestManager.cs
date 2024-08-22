using Projection.Shared.Data.Contexts;
using Projection.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Shared.Data.Idempotency;

public class RequestManager<TContext>(TContext context) : IRequestManager where TContext : BaseDbContext
{
    private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _context.FindAsync<ClientRequest>(id) != null;
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        var exists = await ExistAsync(id);

        if (exists) return;

        var request = new ClientRequest
        {
            Id = id,
            Name = typeof(T).Name,
            Time = DateTime.UtcNow
        };

        _context.Add(request);

        await _context.SaveChangesAsync();
    }
}
