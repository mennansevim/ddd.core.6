using Domain.B2BMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.B2BMasters.Configurations
{
    public class B2BMasterItemConfiguration : IEntityTypeConfiguration<B2BMasterItem>
    {
        public void Configure(EntityTypeBuilder<B2BMasterItem> builder)
        {
            
            builder.ToTable("B2BMasterItem");

            builder.HasKey(x => x.Id);
            
            builder.Property(e => e.B2BMasterId).HasColumnName("B2BId");
            builder.Property(e => e.CreationDate);
            builder.Property(e => e.LastModifiedDate);
            builder.Property(e => e.ReservationId).HasMaxLength(300);
            builder.Property(e => e.StockId).HasMaxLength(300);
            builder.Property(x => x.UnSuppliedQuantity).HasColumnName("UnsuppQuantity");
            builder.Property(x => x.OrderedQuantity);
            builder.Property(x => x.RequestedQuantity);
            builder.Property(x => x.CanceledQuantity);
            builder.Property(x => x.IsRowDeleted);

            builder.OwnsOne(x => x.Product, b =>
            {
                b.Property(x => x.Barcode).HasColumnName("Barcode");
                b.Property(x => x.Composition).HasColumnName("Composition");
                b.Property(x => x.Name).HasColumnName("Name");
                b.Property(x => x.Origin).HasColumnName("Origin");
                b.Property(x => x.HsCode).HasColumnName("HsCode");
                b.Property(x => x.ListingId).HasColumnName("ListingId");
                b.Property(x => x.VariantId).HasColumnName("ProductMainVariantId");
                b.Property(x => x.Weight).HasColumnName("Weight").HasColumnType("decimal(10, 2)");
            });
            
            builder.OwnsOne(x => x.ProductPrice, b =>
            {
                b.Property(e => e.Price).HasColumnName("Price").HasColumnType("decimal(10, 2)");
                b.Property(e => e.PriceVatIncluded).HasColumnName("PriceVatIncluded").HasColumnType("decimal(10, 2)");
                b.Property(x => x.Vat).HasColumnName("Vat").HasColumnType("decimal(10, 2)");
                b.Property(x => x.VatRate).HasColumnName("VatRate");
                b.Property(x => x.Currency).HasColumnName("Currency");
            });

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}