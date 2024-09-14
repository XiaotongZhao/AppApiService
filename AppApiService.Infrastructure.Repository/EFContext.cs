using Microsoft.EntityFrameworkCore;
using AppApiService.Domain.Common;
using AppApiService.Domain.DevOps;
using AppApiService.Domain.DynamicRequestDataService;
using AppApiService.Domain.DevOps.ServiceTask;

namespace AppApiService.Infrastructure.Repository;

public class EFContext : DbContext
{

    public EFContext(DbContextOptions<EFContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
    public virtual DbSet<DataMap> DataMap { get; set; }
    public virtual DbSet<Server> Servers { get; set; }
    public virtual DbSet<ServerUploadFile> ServerUploadFiles { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<ServiceTask> ServiceTasks { get; set; }
    public virtual DbSet<DeployService> DeployServices { get; set; }
    public virtual DbSet<DeployServiceTask> DeployServiceTasks { get; set; }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        var modifiedEntities = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity)
            .ToList();
        var addEntities = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity)
            .ToList();
        foreach (var entity in modifiedEntities)
        {
            var baseEntity = entity as EntityBase<long>;
            if (baseEntity != null)
            {
                baseEntity.LastModifyOn = DateTime.Now;
            }
        }
        foreach (var entity in addEntities)
        {
            var baseEntity = entity as EntityBase<long>;
            if (baseEntity != null)
            {
                baseEntity.CreatedOn = DateTime.Now;
            }
        }
        if (addEntities.Count > 0 || modifiedEntities.Count > 0)
            return base.SaveChanges();
        else
            return 0;
    }
}
