using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Models;

namespace Project_NGO.Data
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
            modelBuilder.Entity<Receipt>().ToTable("Receipts").HasKey(u => u.Id);
            modelBuilder.Entity<Programs>().ToTable("Programs").HasKey(u => u.Id);
            modelBuilder.Entity<Program_Image>().ToTable("Program_Images").HasKey(u => u.Id);
            modelBuilder.Entity<Event>().ToTable("Events").HasKey(u => u.Id);
            modelBuilder.Entity<Category>().ToTable("Categories").HasKey(u => u.Id);
            modelBuilder.Entity<Accounting>().ToTable("Accounting").HasKey(u => u.Id);
            modelBuilder.Entity<About>().ToTable("Abouts").HasKey(u => u.Id);
            modelBuilder.Entity<About_Image>().ToTable("About_Images").HasKey(u => u.Id);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Accounting)
                .WithOne(a => a.Receipt)
                .HasForeignKey<Accounting>(a => a.Id)
                .HasConstraintName("FK_Receipt_Accounting");
            modelBuilder.Entity<Programs>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Programs)
                .HasForeignKey(p => p.Category_Id)
                .HasConstraintName("FK_Category_Program");
            modelBuilder.Entity<Program_Image>()
                .HasOne(pi => pi.Programs)
                .WithMany(p => p.Program_Images)
                .HasForeignKey(pi => pi.Program_Id)
                .HasConstraintName("FK_Program_ProImages");
            modelBuilder.Entity<About_Image>()
                .HasOne(ai => ai.About)
                .WithMany(a => a.About_Images)
                .HasForeignKey(ai => ai.About_Id)
                .HasConstraintName("FK_About_AboutImages");
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.User)
                .WithMany(u => u.Receipt)
                .HasForeignKey(r => r.User_Id)
                .HasConstraintName("FK_Receipt_User");
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Programs)
                .WithMany(p => p.Receipt)
                .HasForeignKey(r => r.Program_Id)
                .HasConstraintName("FK_Receipt_Program");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Programs> Programs { get; set; }
        public DbSet<Program_Image> Program_Images { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<About_Image> Abouts_Images { get; set; }
    }
}