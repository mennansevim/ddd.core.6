using System;
using Domain.B2BMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.B2BMasters.Configurations
{
    public class B2BAddressConfiguration : IEntityTypeConfiguration<B2BAddress>
    {
        public void Configure(EntityTypeBuilder<B2BAddress> builder)
        {
            builder.ToTable("B2BAddress");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.AddressType).HasConversion(
                v => v.ToString(),
                v => (B2BAddressType) Enum.Parse(typeof(B2BAddressType), v)
            );
            builder.Property(e => e.Address).HasMaxLength(255);
            builder.Property(e => e.B2BMasterId).HasColumnName("B2BId");
            builder.Property(e => e.City).HasMaxLength(120);
            builder.Property(e => e.CountryName).HasMaxLength(120);
            builder.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            builder.Property(e => e.CreatorEmail).HasMaxLength(256);
            builder.Property(e => e.District).HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.Property(e => e.FirstName).HasMaxLength(200);
            builder.Property(e => e.FreeTradeZone).HasDefaultValueSql("((0))");
            builder.Property(e => e.FullName).HasMaxLength(400);
            builder.Property(e => e.LastModifiedDate).HasDefaultValueSql("(sysdatetime())");
            builder.Property(e => e.LastName).HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(16);
            
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}