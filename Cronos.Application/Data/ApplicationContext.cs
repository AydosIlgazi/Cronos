
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
        public DbSet<ActivityEntity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public static class ApplicationContextExtensions
    {
        public static IQueryable<T> DisplayedEntities<T>(this DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet
                    .Where(b => b.IsActive == true && b.IsDeleted == false)
                    .OrderBy(b => b.Order)
                    .AsQueryable();
        }
        //Adminin etkinliklerin tüm propertylerini görmesi için.  Gülderen Sungur 29.09.2022
        public static IQueryable<T> ShowAllEntity<T>(this DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet
                   
                    .OrderBy(b => b.Order)
                    .AsQueryable();
        }



    }
}
