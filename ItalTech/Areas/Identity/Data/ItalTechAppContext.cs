using ItalTech.Areas.Admin.Models.RolesGroups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Identity.Data
{
    public class ItalTechAppContext : DbContext
    {
        public DbSet<GroupsMaster> GroupsMaster { get; set; }
        public DbSet<GroupsDetail> GroupsDetail { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<UserRoles> UserWithRoles { get; set; }


        public ItalTechAppContext(DbContextOptions<ItalTechAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupsMaster>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(150);
                entity.HasMany(e => e.GroupsDetails).WithOne(e => e.Master);
            });

            modelBuilder.Entity<GroupsDetail>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.GroupId).IsRequired();
                entity.Property(e => e.RoleId).IsRequired();
                entity.HasOne(e => e.Master).WithMany(e => e.GroupsDetails).HasForeignKey(e => e.GroupId);
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.IdUser).HasMaxLength(450).IsRequired();
                entity.Property(e => e.IdGroup).IsRequired();

            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.IdUser).HasMaxLength(450).IsRequired();
                entity.Property(e => e.IdRole).HasMaxLength(450).IsRequired();

            });
        }


    }
}
