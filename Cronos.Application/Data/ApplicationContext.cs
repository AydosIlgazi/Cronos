
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cronos.Application.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add Message --project .\Cronos.Application -s .\Cronos.Web
            //dotnet ef database update --project .\Cronos.Application -s .\Cronos.Web
        }
        public DbSet<BannerEntity> Banners { get; set; }
        public DbSet<AnnouncementEntity> Announcements { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //23.09.2022 Irem Kesemen
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }

    public static class ApplicationContextExtensions
    {
        public static IQueryable<T> DisplayedEntities<T>(this DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet
                .Where(
                   b => b.IsActive == true 
                    && b.StartDate >= DateTime.Now
                    && b.EndDate >= DateTime.Now)
                .OrderBy(b => b.Order)
                    .AsQueryable();
        }

        public static IQueryable<T> CmsDisplay<T>(this DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet
                //.Where(
                //    b => b.IsActive == true 
                //    && b.StartDate <= DateTime.Now
                //    && b.EndDate >= DateTime.Now)
                .OrderBy(b => b.Order)
            return dbSet.Where(
                    b => b.IsActive == true && b.IsDeleted==false && b.StartDate <= DateTime.Now
                    && b.EndDate >= DateTime.Now).OrderBy(b => b.Order)
                    .AsQueryable();
        }
        public static IQueryable<T> DisplayedEntitiesCms<T>(this DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet.OrderBy(b => b.Order)
                    .AsQueryable();
        }
    }
 
}

