using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonalDiary.Models
{
    public partial class PersonalDiaryContext : DbContext
    {
        public PersonalDiaryContext()
        {
        }

        public PersonalDiaryContext(DbContextOptions<PersonalDiaryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diary> Diary { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diary>(entity =>
            {
                entity.Property(e => e.DiaryId).HasColumnName("DiaryID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiaryTitle).HasMaxLength(50);

                entity.Property(e => e.DiaryTitleContent).HasMaxLength(4000);

                entity.Property(e => e.DiaryType).HasMaxLength(50);

                entity.Property(e => e.IsPublic).HasDefaultValueSql("((0))");

                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Diary)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Diary__UsersID__3B75D760");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UsersName).HasMaxLength(50);

                entity.Property(e => e.UsersPhone).HasMaxLength(11);

                entity.Property(e => e.UsersPhoto).HasMaxLength(50);

                entity.Property(e => e.UsersPwd).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
