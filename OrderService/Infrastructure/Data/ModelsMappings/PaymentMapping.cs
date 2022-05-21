using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelsMappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreationDate)
                   .IsRequired();

            builder.Property(p => p.LastUpdateDate)
                   .IsRequired()
                   .ValueGeneratedOnUpdate();

            builder.HasOne(p => p.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
