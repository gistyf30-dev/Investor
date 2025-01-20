using InvestorsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestorsAPI.Database
{
    public class InvestorDbContext: DbContext
    {
        public InvestorDbContext(DbContextOptions<InvestorDbContext> options)
            : base(options)
        {
            //Database.Migrate();
        }
        public virtual DbSet<Investor> Investors { get; set; }
        public virtual DbSet<Fund> Funds { get; set; }
        public virtual DbSet<InvestorFund> InvestorFunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvestorFund>()
                        .HasKey(bc => new { bc.InvestorId, bc.FundId });

            modelBuilder.Entity<InvestorFund>()
                .HasOne(ab => ab.Investor)
                .WithMany(a => a.InvestorFunds)
                .HasForeignKey(ab => ab.InvestorId);

            modelBuilder.Entity<InvestorFund>()
                .HasOne(ab => ab.Fund)
                .WithMany(b => b.InvestorFunds)
                .HasForeignKey(ab => ab.FundId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
