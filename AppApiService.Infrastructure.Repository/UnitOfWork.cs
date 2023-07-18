using AppApiService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AppApiService.Infrastructure.Repository;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private EFContext dbContext;
    private bool disposed;
    public UnitOfWork(EFContext efContext)
    {
        dbContext = efContext;
    }

    public void Dispose()
    {
        if (dbContext != null)
        {
            if (disposed)
                return;
            disposed = true;
            //dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }

    public DbContext Get()
    {
        return dbContext;
    }
}
