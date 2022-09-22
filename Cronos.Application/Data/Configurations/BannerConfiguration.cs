using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cronos.Application.Data.Configurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<BannerEntity>
    {
        public void Configure(EntityTypeBuilder<BannerEntity> builder)
        {
            //builder.Property(b => b.ImageUrl).IsRequired(false);
            //builder.Property(b => b.AltText).HasMaxLength(200);

        }
    }
}
