using CompData.Models.Config;
using CompData.Models.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Models.Identity;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRMData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        #region Identity

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenus { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaims { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        public DbSet<ApplicationLoginAudit> ApplicationLoginAudits { get; set; }
        public DbSet<ApplicationPasswordLog> ApplicationPasswordLogs { get; set; }
        public DbSet<ApplicationIP> ApplicationIPs { get; set; }
        public DbSet<ApplicationUserNotification> ApplicationUserNotifications { get; set; }

        #endregion Identity

        #region Library
        public virtual DbSet<Regulation> Regulations { get; set; }
        public virtual DbSet<RegulationDetail> RegulationDetails { get; set; }
        public virtual DbSet<RegulationSection> RegulationSections { get; set; }
        public virtual DbSet<RegulationSource> RegulationSources { get; set; }
        public virtual DbSet<RegulationType> RegulationTypes { get; set; }
        public virtual DbSet<LinkedUserRegulationSource> LinkedUserRegulationSources { get; set; }
        public virtual DbSet<LinkUserRegTypeSubscription> LinkUserRegTypeSubscriptions { get; set; }
        public virtual DbSet<LinkUserRegulationSubscription> LinkUserRegulationSubscriptions { get; set; }
        public virtual DbSet<LinkedRelatedRegulation> LinkedRelatedRegulations { get; set; }
        public virtual DbSet<TagMap> TagMaps { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        #endregion

        #region Config
        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<OrganizationDomain> OrganizationDomains { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        #endregion

        #region ProcedureView
        public virtual DbSet<UserAccessibleClaims> UserAccessibleClaims { get; set; }
        public virtual DbSet<RegulationGroupBySourceProcedure> RegulationGroupBySources { get; set; }
        public virtual DbSet<SelectedRegulationProcedure> SelectedRegulation { get; set; }
        public virtual DbSet<RegulationFilteredBySource> RegulationFilteredBySource { get; set; }
        public virtual DbSet<SelectedRegulationRequirement> SelectedRegRequirement { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Default Identity Table
            builder.Entity<ApplicationUser>(x =>
                    x.ToTable("ApplicationUser", "Security")
                );

            builder.Entity<ApplicationRole>(x =>
                x.ToTable("ApplicationRole", "Security")
            );

            builder.Entity<ApplicationUserClaim>(x =>
                x.ToTable("ApplicationUserClaim", "Security")
            );

            builder.Entity<ApplicationUserRole>(x =>
                x.ToTable("ApplicationUserRole", "Security")
            );

            builder.Entity<ApplicationUserLogin>(x =>
                x.ToTable("ApplicationUserLogin", "Security")
            );

            builder.Entity<ApplicationRoleClaim>(x =>
            {
                x.Property(e => e.ClaimType).HasMaxLength(100);
                x.ToTable("ApplicationRoleClaim", "Security");
                x.HasOne<ApplicationClaim>(e => e.ApplicationClaim)
                .WithMany(e => e.ApplicationRoleClaim)
                .HasForeignKey(e => e.ClaimType)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ApplicationUserToken>(x =>
                x.ToTable("ApplicationUserToken", "Security")
            );

            builder.Entity<ApplicationClaim>(x =>
            {
                x.HasKey(e => e.Claim);
            });

            builder.Entity<ApplicationMenu>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                x.Property(e => e.ParentId).IsRequired(false);
                x.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

                x.HasOne<ApplicationClaim>(e => e.ApplicationClaim)
                .WithMany(e => e.ApplicationMenu)
                .HasForeignKey(x => x.ClaimID)
                .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region Library
            builder.Entity<RegulationSection>(x =>
            {
                x.HasKey(e => e.SectionId);
                x.Property(e => e.SectionId).ValueGeneratedOnAdd();
                x.Property(e => e.ParentId).IsRequired(false);
                x.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

                x.HasOne<Regulation>(e => e.Regulation)
                .WithMany(e => e.RegulationSections)
                .HasForeignKey(x => x.RegulationId)
                .OnDelete(DeleteBehavior.Restrict);

                x.HasMany<RegulationDetail>(e => e.RegulationDetails)
                .WithOne(x => x.RegulationSection)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<LinkedRelatedRegulation>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                
                x.HasOne<Regulation>(e => e.Regulation)
                .WithMany(e => e.LinkedRegulations)
                .HasForeignKey(x => x.RegId)
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne<Regulation>(e => e.RelatedRegulation)
                .WithMany(e => e.RelatedRegulations)
                .HasForeignKey(x => x.RelatedRegId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TagMap>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).ValueGeneratedOnAdd();
                x.HasIndex(e => e.TagGroupKey);

                x.HasOne<Regulation>(e => e.Regulation)
                .WithMany(e => e.TagMaps)
                .HasForeignKey(x => x.RegId)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasOne<RegulationSection>(e => e.RegulationSection)
                .WithMany(x => x.TagMaps)
                .HasForeignKey(x => x.SecId)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasOne<RegulationDetail>(e => e.RegulationDetail)
                .WithMany(x => x.TagMaps)
                .HasForeignKey(x => x.DescId)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasOne<TagMapType>(e => e.TagMapType)
                .WithMany(x => x.TagMaps)
                .HasForeignKey(x => x.TagType)
                .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #region ProcedureView
            builder.Entity<UserAccessibleClaims>().HasNoKey();
            builder.Ignore<XMLViewModel>();
            builder.Entity<RegulationGroupBySourceProcedure>().HasNoKey();
            builder.Entity<SelectedRegulationProcedure>().HasNoKey();
            builder.Entity<RegulationFilteredBySource>().HasNoKey();
            builder.Entity<SelectedRegulationRequirement>().HasNoKey();
            #endregion
        }

        public override int SaveChanges()
        {
            //var changedEntities = ChangeTracker
            //.Entries()
            //.Where(_ => _.State == EntityState.Added ||
            //            _.State == EntityState.Modified);
            //
            //var errors = new List<ValidationResult>(); // all errors are here
            //foreach (var e in changedEntities)
            //{
            //    var vc = new ValidationContext(e.Entity, null, null);
            //    Validator.TryValidateObject(
            //        e.Entity, vc, errors, validateAllProperties: true);
            //}

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //var changedEntities = ChangeTracker
            //.Entries()
            //.Where(_ => _.State == EntityState.Added ||
            //            _.State == EntityState.Modified);
            //
            //var errors = new List<ValidationResult>(); // all errors are here
            //foreach (var e in changedEntities)
            //{
            //    var vc = new ValidationContext(e.Entity, null, null);
            //    Validator.TryValidateObject(
            //        e.Entity, vc, errors, validateAllProperties: true);
            //}

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
