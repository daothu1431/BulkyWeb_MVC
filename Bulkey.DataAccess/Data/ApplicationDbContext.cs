using Bulkey.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulkey.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Tạo bảng Categories
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Quá trình chèn dữ liệu mẫu vào database khi khởi tạo ứng dụng.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Action", DisplayOrder = 1},
                    new Category { Id = 2, Name = "Method", DisplayOrder = 2},
                    new Category { Id = 3, Name = "Control", DisplayOrder = 3}
             );

            modelBuilder.Entity<Product>().HasData(
                    new Product { 
                        Id = 1, Title = "Action",
                        Description = "Hello",
                        ISBN = "SWD9999001",
                        Author = "Dao Thu",
                        ListPrice = 90,
                        Price = 90,
                        Price50 = 90,
                        Price100 = 90,
                        CategoryId = 16,
                        ImageUrl = "http://",
                    }
             );
        }
    }
}
