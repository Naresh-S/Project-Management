namespace ProjectManagement.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext()
            : base("name=ProjectManagementContext")
        {
        }

        public virtual DbSet<ParentTask> ParentTasks { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentTask>()
                .Property(e => e.Parent_Task)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                 .Property(e => e.TaskName)
                 .IsUnicode(false);



            modelBuilder.Entity<Task>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);
            modelBuilder.Entity<ParentTask>()
              .Property(e => e.Parent_Task)
              .IsUnicode(false);
        }
    }
}
