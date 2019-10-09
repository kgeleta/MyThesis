using Microsoft.EntityFrameworkCore;
using UnityFeedback.Configuration;

namespace UnityFeedback.Models
{
	public partial class Unity1Context : DbContext
    {
        public Unity1Context()
        {
		}

        public Unity1Context(DbContextOptions<Unity1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Floppybird> Floppybird { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
	            var connectionString = AppSettings.Instance.ConnectionString;
				optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Floppybird>(entity =>
            {
                entity.ToTable("floppybird");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Enjoyed).HasColumnName("enjoyed");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });
        }
    }
}
