using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulkey.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Nhập từ 1 - 100 thôi")]
        public int DisplayOrder { get; set; }
    }
}

// Required => khi bảng trong SQL được tạo nó sẽ được cài dặt là NOT NULL (Bắt buộc phải nhập, không được để trống)