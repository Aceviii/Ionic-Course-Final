using Microsoft.EntityFrameworkCore;

namespace task_api.Migrations
{
    using MyTask = task_api.Models.Task;

    public class TaskDbContext : DbContext
    {
        public DbSet<MyTask> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);
                entity.Property(e => e.TaskId)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Completed).IsRequired();
            });

            modelBuilder.Entity<MyTask>().HasData(
           new MyTask
           {
               TaskId = 1,
               Title = "Decide on a database",
               Completed = true,
           },
           new MyTask
           {
               TaskId = 2,
               Title = "Decide on using C# or Node.js",
               Completed = true,
           }
       );

            
            modelBuilder.HasSequence<int>("TaskIdSequence", schema: "dbo")
                .StartsAt(3); 

            modelBuilder.Entity<MyTask>().Property(p => p.TaskId)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.TaskIdSequence");
        }
    }
}