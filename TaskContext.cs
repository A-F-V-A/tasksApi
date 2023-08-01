using CourseEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CourseEF
{
    public class TaskContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categoriasInit = new List<Category>();
            categoriasInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Name = "Actividades pendientes", Difficulty = 20 });
            categoriasInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Name = "Actividades personales", Difficulty = 50 });

            modelBuilder.Entity<Category>(cat =>
            {
                cat.ToTable("Category");
                cat.HasKey(c => c.CategoryId);
                cat.Property(p => p.Name).IsRequired().HasMaxLength(150);
                cat.Property(p => p.Description).IsRequired(false);
                cat.Property(p=> p.Difficulty);
                cat.HasData(categoriasInit);
            });

            List<Tasks> tareasInit = new List<Tasks>();
            tareasInit.Add(new Tasks() { TasksId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Priority = Priority.HIGHT, Title = "Pago de servicios publicos", CreationDate = DateTime.Now });
            tareasInit.Add(new Tasks() { TasksId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Priority = Priority.LOW, Title = "Terminar de ver pelicula en netflix", CreationDate = DateTime.Now });


            modelBuilder.Entity<Tasks>(task => {
                task.ToTable("Task");
                task.HasOne(ho => ho.Category).WithMany(wm => wm.Tasks).HasForeignKey(hf => hf.CategoryId);
                task.Property(t => t.Title).IsRequired().HasMaxLength(200);
                task.Property(t => t.Description).HasMaxLength(500);
                task.Property(t => t.Priority).IsRequired();
                task.Property(t => t.CreationDate);
                task.HasData(tareasInit);
            });
        }

    }
}
