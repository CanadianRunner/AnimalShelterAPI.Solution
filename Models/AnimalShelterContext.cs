using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Models
{
    public class AnimalShelterContext : DbContext
    {
        public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Animal>()
                .HasData(
                    new Animal { AnimalId = 1, Name = "Jack", Species = "Dog", Age = 1, Gender = "Male" },
                    new Animal { AnimalId = 2, Name = "Roxie", Species = "Dog", Age = 3, Gender = "Female" },
                    new Animal { AnimalId = 3, Name = "Chloe", Species = "Dog", Age = 12, Gender = "Female" },
                    new Animal { AnimalId = 4, Name = "Tibbs", Species = "Cat", Age = 8, Gender = "Male" },
                    new Animal { AnimalId = 5, Name = "Misty", Species = "Cat", Age = 15, Gender = "Male" },
                    new Animal { AnimalId = 6, Name = "Lily", Species = "Cat", Age = 18, Gender = "Female" }
                );
        }
        
    }
}