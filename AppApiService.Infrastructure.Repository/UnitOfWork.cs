﻿using Microsoft.EntityFrameworkCore;
using AppApiService.Domain.Common;

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
            dbContext.Dispose();
        }
    }

    public DbContext Get()
    {
        return dbContext;
    }
}
