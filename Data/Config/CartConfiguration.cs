using Microsoft.EntityFrameworkCore;
using KedaiAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KedaiAPI.Data.Config
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(b => b.CartId);
            builder.Property(e => e.CartId).HasColumnName("CartId");

            builder.Property(e => e.Quantities).HasColumnName("Quantities");
            builder.Property(e => e.isDeleted).HasColumnName("isDeleted");

            builder.Property(e => e.ProductId).HasColumnName("ProductId");
            builder.Property(e => e.UserId).HasColumnName("UserId");


            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
