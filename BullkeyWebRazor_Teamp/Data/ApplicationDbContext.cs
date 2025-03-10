using BullkeyWebRazor_Teamp.Models;
using Microsoft.EntityFrameworkCore;

namespace BullkeyWebRazor_Teamp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Tạo bảng Categories
        public DbSet<Category> Categories { get; set; }

        // Quá trình chèn dữ liệu mẫu vào database khi khởi tạo ứng dụng.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "Method", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "Control", DisplayOrder = 3 }
                );
        }
    }
}
