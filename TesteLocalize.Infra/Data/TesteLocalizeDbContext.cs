
using Microsoft.EntityFrameworkCore;
using TesteLocalize.Domain.Entities;

namespace TesteLocalize.Infra.Data
{
    public class TesteLocalizeDbContext : DbContext
    {
        public TesteLocalizeDbContext(DbContextOptions<TesteLocalizeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
                entity.Property(e => e.FantasyName).HasMaxLength(250);
                entity.Property(e => e.CNPJ).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Situation).HasMaxLength(50);
                entity.Property(e => e.Type).HasMaxLength(50);
                entity.Property(e => e.LegalNature).HasMaxLength(150);
                entity.Property(e => e.MainActivity).HasMaxLength(250);
                entity.Property(e => e.Street).HasMaxLength(250);
                entity.Property(e => e.Number).HasMaxLength(20);
                entity.Property(e => e.Complement).HasMaxLength(250);
                entity.Property(e => e.Neighborhood).HasMaxLength(150);
                entity.Property(e => e.City).HasMaxLength(150);
                entity.Property(e => e.State).HasMaxLength(2);
                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasIndex(e => new { e.CNPJ, e.UserId })
                    .IsUnique();

                entity.HasOne<User>()
                    .WithMany(u => u.Companies)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
