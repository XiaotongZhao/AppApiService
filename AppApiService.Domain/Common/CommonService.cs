namespace AppApiService.Domain.Common;

public class CommonService<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    private IUnitOfWork unitOfWork;

    public CommonService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Add(TEntity entity)
    {
        await unitOfWork.Get().Set<TEntity>().AddAsync(entity);
        var changeCount = await unitOfWork.Get().SaveChangesAsync();
        return changeCount > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await unitOfWork.Get().Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            unitOfWork.Get().Set<TEntity>().Update(entity);
            var changeCount = await unitOfWork.Get().SaveChangesAsync();
            return changeCount >= 0;
        }
        else
            return false;
    }

    public async Task<List<TEntity>> GetDataListAsync()
    {
        var datas = await unitOfWork.Get().Set<TEntity>().ToListAsync();
        return datas;
    }

    public async Task<bool> Update(TEntity entity)
    {
        unitOfWork.Get().Set<TEntity>().Update(entity);
        var changeCount = await unitOfWork.Get().SaveChangesAsync();
        return changeCount >= 0;
    }
}
