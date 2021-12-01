using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnicdaPlatform.Models.CareerSubjects;
using UnicdaPlatform.Models.Company;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.Request;
using UnicdaPlatform.Models.Trans;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        #region CareerSubjects
        public DbSet<Career> Career { get; set; }
        public DbSet<CareerPensum> CareerPensum { get; set; }
        public DbSet<CareerUserPensum> CareerUserPensum { get; set; }
        public DbSet<CareerUserPensumDetails> CareerUserPensumDetails { get; set; }
        public DbSet<CareerUserTeacherPensum> CareerUserTeacherPensum { get; set; }
        public DbSet<Matter> Matter { get; set; }
        public DbSet<UserMatter> UserMatter { get; set; }
        #endregion

        #region Request
        public DbSet<RequestUserChangeCareer> RequestUserChangeCareer { get; set; }
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

            modelBuilder.Entity<Career>().HasIndex(a => a.Id);
            modelBuilder.Entity<CareerPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<CareerUserPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<CareerUserPensumDetails>().HasIndex(a => a.Id);
            modelBuilder.Entity<CareerUserTeacherPensum>().HasIndex(a => a.Id);
            modelBuilder.Entity<UserMatter>().HasIndex(a => a.Id);
            modelBuilder.Entity<Matter>().HasIndex(a => a.Id);

            modelBuilder.Entity<RequestUserChangeCareer>().HasIndex(a => a.Id);
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
