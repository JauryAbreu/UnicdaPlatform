using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnicdaPlatform.Models.CarrierSubjects;
using UnicdaPlatform.Models.Company;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.Request;
using UnicdaPlatform.Models.Trans;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        #region CarrierSubjects
        public DbSet<Carrier> Carrier { get; set; }
        public DbSet<CarrierPensum> CarrierPensum { get; set; }
        public DbSet<CarrierUserPensum> CarrierUserPensum { get; set; }
        public DbSet<CarrierUserPensumDetails> CarrierUserPensumDetails { get; set; }
        public DbSet<CarrierUserTeacherPensum> CarrierUserTeacherPensum { get; set; }
        public DbSet<UserMatter> UserMatter { get; set; }
        #endregion

        #region Request
        public DbSet<RequestUserChangeCarrier> RequestUserChangeCarrier { get; set; }
        public DbSet<RequestUserMatter> RequestUserMatter { get; set; }
        public DbSet<Complaints> Complaints { get; set; }

        #endregion

        #region Company
        public DbSet<Company> Company { get; set; }
        #endregion

        #region Ncf
        public DbSet<NcfHistory> NcfHistory { get; set; }
        public DbSet<NcfSequenceDetail> NcfSequenceDetail { get; set; }
        public DbSet<NcfType> NcfType { get; set; }
        #endregion

        #region Transaction
        public DbSet<Batch> Batch { get; set; }
        public DbSet<Transactions> Transaction { get; set; }
        public DbSet<TransactionsDetails> TransactionsDetails { get; set; }
        #endregion

        #region User
        public DbSet<GroupPermission> GroupPermission { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().HasIndex(a => a.Id);

            modelBuilder.Entity<Carrier>().HasIndex(a => a.Id);
            modelBuilder.Entity<CarrierPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<CarrierUserPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<CarrierUserPensumDetails>().HasIndex(a => a.Id);
            modelBuilder.Entity<CarrierUserTeacherPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<UserMatter>().HasIndex(a => a.Id);

            modelBuilder.Entity<RequestUserChangeCarrier>().HasIndex(a => a.Id);
            modelBuilder.Entity<RequestUserMatter>().HasIndex(a => a.Id);
            modelBuilder.Entity<Complaints>().HasIndex(a => a.Id);

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
