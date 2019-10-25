using Microsoft.EntityFrameworkCore;
using repository.entity.db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace repository.operation
{
    public class DBContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }

        public DbSet<Source> Sources { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<TopicCategory> TopicCategories { get; set; }

        public static LoggerFactory MyLoggerFactory { get; } = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level)
            => category == DbLoggerCategory.Database.Command.Name
                && level == _loggingLevel,true) });

        private static LogLevel _loggingLevel;

        public DBContext()
        {

        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(b => b.Name)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.TopicCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            //TopicCategories

            modelBuilder.Entity<TopicCategory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<TopicCategory>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TopicCategory>()
                .ToTable("TopicCategories");
            //Sources

            modelBuilder.Entity<Source>()
                .Property(b => b.Url)
                .IsRequired();

            modelBuilder.Entity<Source>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Source>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Source>()
                .Property(b => b.Name)
                .IsRequired();

            modelBuilder.Entity<Source>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Source>()
                .Property(b => b.ItemParsePath)
                .IsRequired();


            modelBuilder.Entity<Source>()
                .HasMany<Topic>(s => s.Topics)
                .WithOne(t => t.Source);

            modelBuilder.Entity<Source>()
                .ToTable("Sources");


            //topics

            modelBuilder.Entity<Topic>()
                .Property(b => b.Title)
                .IsRequired();

            modelBuilder.Entity<Topic>()
                .HasKey(b => b.Guid);

            modelBuilder.Entity<Topic>()
                .Property(b => b.PublisDate)
                .IsRequired();


            modelBuilder.Entity<Topic>()
                .Property(b => b.Description)
                .IsRequired();

            modelBuilder.Entity<Topic>()
                .Property(b => b.Link)
                .IsRequired();
            

            modelBuilder.Entity<Topic>()
                .HasOne(s => s.Source)
                .WithMany(t => t.Topics);

            modelBuilder.Entity<Topic>()
                .HasMany(s => s.TopicCategories)
                .WithOne(t => t.Topic);

            modelBuilder.Entity<Topic>()
                .ToTable("Topics");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();

            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", true, true)
             .Build();

            Enum.TryParse(config["SQLDebugLevel"], out _loggingLevel);

            optionsBuilder.UseSqlServer($"{config["ConnectionString"]}");
            

            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging();
        }
    }
}
