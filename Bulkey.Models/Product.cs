using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulkey.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        // Giá mặc định
        [Required]
        [DisplayName("List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        // Mức giá từ 1 - 50
        [Required]
        [DisplayName("Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        // mức giá khi mua trên 50 sản phẩm
        [Required]
        [DisplayName("Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        // Mức giá khi mua trên 100 sp
        [Required]
        [DisplayName("Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }


        // Cấu hình khóa ngoại cho bảng sản phẩm
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [DisplayName("Category")]
        public Category Category { get; set; }

        // add-migration addForeignKeyForCategoryProductRelation

        [Required]
        public string ImageUrl { get; set; }
        // add-migration addImageUrlToProduct

    }
}
