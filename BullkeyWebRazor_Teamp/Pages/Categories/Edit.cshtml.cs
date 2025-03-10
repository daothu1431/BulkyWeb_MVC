using BullkeyWebRazor_Teamp.Data;
using BullkeyWebRazor_Teamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkeyWebRazor_Teamp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        // prop
        [BindProperty]
        public Category? Category { get; set; }
        //ctor
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        // Cập nhật dữ liệu
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                // Hiiển thị thông báo đã update thành công
                TempData["success"] = "Cập nhật dữ liệu thành công";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
