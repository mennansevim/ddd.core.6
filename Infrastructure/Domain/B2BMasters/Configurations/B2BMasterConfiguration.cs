using System;
using Domain.B2BMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.B2BMasters.Configurations
{
    public class B2BMasterConfiguration : IEntityTypeConfiguration<B2BMaster>
    {
        public void Configure(EntityTypeBuilder<B2BMaster> builder)
        {
            builder.ToTable("B2BMasters");

            builder.HasKey(x => x.Id);
            builder.Property(e => e.OrderParentId).HasColumnName("B2BParentId");
            builder.Property(x => x.WarehouseId);
            builder.Property(e => e.CarrierType).HasConversion(
                v => v.Value,
                v => CarrierType.FromValue<CarrierType>(v)
            );
            builder.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            builder.Property(e => e.CreatorEmail).HasMaxLength(256);
            builder.Property(e => e.InvoiceTypeName).HasMaxLength(30).IsUnicode(false);
            builder.Property(e => e.IsInvoiced).HasDefaultValueSql("((0))");
            builder.Property(e => e.LastModifiedDate).HasDefaultValueSql("(sysdatetime())");

            builder.Property(e => e.Status).HasColumnName("StatusName")
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<B2BMasterStatus>(v))
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Type).HasColumnName("TypeName").HasConversion(
                v => v.ToString(),
                v => Enum.Parse<B2BType>(v)
            );

            builder.OwnsOne(x => x.Supplier, b =>
            {
                b.Property(x => x.Id).HasColumnName("SupplierId");
                b.Property(x => x.Name).HasColumnName("SupplierName").IsRequired().HasMaxLength(500);
            });

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}