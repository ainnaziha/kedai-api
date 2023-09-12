using KedaiAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KedaiAPI.Data.Config
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            //  builder.Property(P => P.Id).IsRequired();
            builder.Property(N => N.Name).IsRequired().HasMaxLength(100);
            builder.Property(N => N.Description).IsRequired();
            builder.Property(P => P.Image).IsRequired(false);
        }
    }
}
