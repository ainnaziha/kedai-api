using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KedaiAPI.Models;


namespace KedaiAPI.Data.Config
{
	internal class ProductCategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories"); 
			builder.HasKey(b => b.CategoryId);
			builder.Property(e => e.CategoryId).HasColumnName("CategoryId");
			builder.Property(e => e.CategoryName)
					.IsRequired()
					.HasMaxLength(255)
					.IsUnicode(false)
					.HasColumnName("CategoryName");
		}
	}
}
