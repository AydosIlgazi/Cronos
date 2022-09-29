using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cronos.Application.Data.Configurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<BannerEntity>
    {
        public void Configure(EntityTypeBuilder<BannerEntity> builder)
        {
            //builder.Property(b => b.ImageUrl).IsRequired(true);
            //builder.Property(b => b.AltText).HasMaxLength(10);
           // builder.Property(b => b.ModifiedDate).WithMessage("campo obrigatório");
            //.LessThan(p => DateTime.Now).WithMessage("a data deve estar no passado");



        }
    }
}
