using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnicdaPlatform.Models.Company;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.Trans;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Company> Company { get; set; }


        public DbSet<NcfHistory> NcfHistory { get; set; }
        public DbSet<NcfSequenceDetail> NcfSequenceDetail { get; set; }
        public DbSet<NcfType> NcfType { get; set; }

        public DbSet<Batch> Batch { get; set; }
        public DbSet<Transactions> Transaction { get; set; }
        public DbSet<TransactionsDetails> TransactionsDetails { get; set; }


        public DbSet<GroupPermission> GroupPermission { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().HasIndex(a => a.Id);

            modelBuilder.Entity<NcfHistory>().HasIndex(a => a.Id);
            modelBuilder.Entity<NcfSequenceDetail>().HasIndex(a => a.Id);
            modelBuilder.Entity<NcfType>().HasIndex(a => a.NcfId);

            modelBuilder.Entity<Batch>().HasIndex(a => a.Id);
            modelBuilder.Entity<Transactions>().HasIndex(a => a.Id);
            modelBuilder.Entity<TransactionsDetails>().HasIndex(a => a.Id);


            modelBuilder.Entity<GroupPermission>().HasIndex(a => a.Id);
            modelBuilder.Entity<Permission>().HasIndex(a => a.Id);
            modelBuilder.Entity<User>().HasIndex(a => a.Id);
            modelBuilder.Entity<UserGroup>().HasIndex(a => a.Id);
        }
    }
}
