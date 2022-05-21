using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelsMappings
{
    public class CreditCardMapping : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable(nameof(CreditCard));

            builder.Property(c => c.Number)
                   .HasColumnType("VARCHAR(20)")
                   .IsRequired();

            builder.Property(c => c.CVV)
                   .HasColumnType("VARCHAR(3)")
                   .IsRequired();

            builder.Property(c => c.NumberOfInstallment)
                   .IsRequired();

            builder.Property(c => c.ValuePerInstallment)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();
        }
    }
}
