using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreatedOn)
                   .IsRequired();

            builder.Property(o => o.LastUpdate)
                   .IsRequired();

            builder.Property(c => c.ProductDescription)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(c => c.ProductValue)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.Property(c => c.ProductQuantity)
                   .IsRequired();

            builder.Property(o => o.UserEmail)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .IsRequired();
        }
    }
}
