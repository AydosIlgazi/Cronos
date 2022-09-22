using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cronos.Application.Data.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<AnnouncementEntity>
    {
        public void Configure(EntityTypeBuilder<AnnouncementEntity> builder)
        {
            builder.Property(b => b.ShortDescription).HasMaxLength(200);

        }
    }
}
