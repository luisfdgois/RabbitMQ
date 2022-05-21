using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelsMappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreationDate)
                   .IsRequired();

            builder.Property(o => o.LastUpdateDate)
                   .IsRequired()
                   .ValueGeneratedOnUpdate();

            builder.Property(c => c.ProductDescription)
                   .HasColumnType("VARCHAR(200)")
                   .IsRequired();

            builder.Property(c => c.ProductValue)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.Property(c => c.ProductQuantity)
                   .IsRequired();

            builder.Property(o => o.UserEmail)
                   .HasColumnType("VARCHAR(60)")
                   .IsRequired();
        }
    }
}
