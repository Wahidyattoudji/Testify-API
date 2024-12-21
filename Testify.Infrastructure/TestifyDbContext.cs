using Microsoft.EntityFrameworkCore;
using Testify.Core.Models;

namespace Testify.Core;

public partial class TestifyDbContext : DbContext
{
    public TestifyDbContext()
    {
    }

    public TestifyDbContext(DbContextOptions<TestifyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionAnswer> SubmissionAnswers { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=TestifyDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.Property(e => e.EvaluatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.Submission).WithMany(p => p.Evaluations).HasForeignKey(d => d.SubmissionId);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasOne(d => d.Test).WithMany(p => p.Questions).HasForeignKey(d => d.TestId);
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.OptionId);

            entity.Property(e => e.IsCorrect).HasColumnType("BOOLEAN");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions).HasForeignKey(d => d.QuestionId);
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.Student).WithMany(p => p.Submissions).HasForeignKey(d => d.StudentId);

            entity.HasOne(d => d.Test).WithMany(p => p.Submissions).HasForeignKey(d => d.TestId);
        });

        modelBuilder.Entity<SubmissionAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId);

            entity.HasOne(d => d.Question).WithMany(p => p.SubmissionAnswers).HasForeignKey(d => d.QuestionId);

            entity.HasOne(d => d.SelectedOption).WithMany(p => p.SubmissionAnswers).HasForeignKey(d => d.SelectedOptionId);

            entity.HasOne(d => d.Submission).WithMany(p => p.SubmissionAnswers).HasForeignKey(d => d.SubmissionId);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
