using Application.Common.Outbox;
using Domain.B2BMasters;
using Domain.PreReservations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Common.Outbox;
using Infrastructure.Domain.B2BMasters.Configurations;

namespace Infrastructure.Persistence
{
    public partial class B2BContext : DbContext
    {
        public B2BContext()
        {
        }

        public B2BContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public virtual DbSet<B2BMaster> B2BMasters { get; set; }
        public virtual DbSet<B2BStatusHistory?> B2BStatusHistories { get; set; }
        public virtual DbSet<B2BAddress> B2BAddresses { get; set; }
        public virtual DbSet<B2BMasterItem> B2BMasterItems { get; set; }
        public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");
            modelBuilder.ApplyConfiguration(new B2BMasterConfiguration());
            modelBuilder.ApplyConfiguration(new B2BAddressConfiguration());
            modelBuilder.ApplyConfiguration(new B2BMasterItemConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}