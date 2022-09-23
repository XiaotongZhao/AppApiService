namespace AppApiService.Domain.Common;

public interface IUnitOfWork
{
    DbContext Get();
}

