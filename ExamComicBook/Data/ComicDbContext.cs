namespace ExamComicBook.Data
{
    using ExamComicBook.Models;
    using Microsoft.EntityFrameworkCore;

    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
    

        public static void SeedData(ComicDbContext context)
        {
            // Check if the Customers table is empty
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { FullName = "John Doe", PhoneNumber = "1234567890" },
                    new Customer { FullName = "Jane Smith", PhoneNumber = "0987654321" }
                );
                context.SaveChanges();  // Save data to the database
            }

            // Add other seed data for ComicBooks, Rentals, etc. in a similar manner
        }
    }
}