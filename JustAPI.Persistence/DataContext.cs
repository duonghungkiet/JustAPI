using JustAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace JustAPI.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Department

            modelBuilder.Entity<Department>(department =>
            {
                _ = department.HasIndex(i => new { i.Code, i.Name })
                .IsUnique(true);

                _ = department.Property(i => i.Id).ValueGeneratedOnAdd();

                _ = department.Property(i => i.Name)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();

                _ = department.Property(i => i.Code)
                    .HasColumnType("nvarchar(10)")
                    .IsRequired();

                _ = department.Property(i => i.Description)
                    .HasColumnType("nvarchar(1000)");

                _ = department.Property(i => i.Created).ValueGeneratedOnAdd();

                _ = department.Property(i => i.Updated).ValueGeneratedOnUpdate();
            });

            #endregion

            #region Group

            _ = modelBuilder.Entity<Group>(group =>
            {
                _ = group.HasIndex(i => new { i.Code, i.Name })
                    .IsUnique(true);

                _ = group.Property(i => i.Id).ValueGeneratedOnAdd();

                _ = group.Property(i => i.Name)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();

                _ = group.Property(i => i.Code)
                    .HasColumnType("nvarchar(10)")
                    .IsRequired();

                _ = group.Property(i => i.Description)
                    .HasColumnType("nvarchar(1000)");

                _ = group.Property(i => i.Created).ValueGeneratedOnAdd();

                _ = group.Property(i => i.Updated).ValueGeneratedOnUpdate();

                _ = group.HasOne<Department>(d => d.Department)
                    .WithMany(g => g.Groups)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region User

            modelBuilder.Entity<User>(user =>
            {
                _ = user.HasIndex(i => new { i.Name, i.Email, i.LoginName });

                _ = user.Property(i => i.Id).ValueGeneratedOnAdd();

                _ = user.Property(i => i.Name )
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();
                
                _ = user.Property(i => i.Email )
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();

                _ = user.Property(i => i.LoginName)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                _ = user.Property(i => i.Password)
                    .HasColumnType("nvarchar(2500)")
                    .IsRequired();
                
                _ = user.Property(i => i.Signature)
                    .HasColumnType("nvarchar(2500)")
                    .IsRequired();

                _ = user.Property(i => i.Created).ValueGeneratedOnAdd();

                _ = user.Property(i => i.Updated).ValueGeneratedOnUpdate();

                _ = user.Property(i => i.GroupId).HasDefaultValue(null);

                _ = user.HasOne(g => g.Group)
                    .WithMany(u => u.Users)
                    .HasForeignKey(g => g.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                _ = user.HasOne(r => r.Role)
                    .WithMany(u => u.Users)
                    .HasForeignKey(r => r.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Role

            modelBuilder.Entity<Role>(role => 
            {
                _ = role.HasIndex(i => i.Name);

                _ = role.Property(i => i.Id).ValueGeneratedOnAdd();

                _ = role.Property(i => i.Name)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();

                _ = role.Property(i => i.Description)
                    .HasColumnType("nvarchar(1000)");

                _ = role.Property(i => i.IsAdmin).HasDefaultValue(false);

                _ = role.Property(i => i.IsManager).HasDefaultValue(false);

                _ = role.Property(i => i.IsUser).HasDefaultValue(true);
            });

            #endregion
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
