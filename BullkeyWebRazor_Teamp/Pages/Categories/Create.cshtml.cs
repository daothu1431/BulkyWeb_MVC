using BullkeyWebRazor_Teamp.Data;
using BullkeyWebRazor_Teamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkeyWebRazor_Teamp.Pages.Categories
{
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        // prop
        [BindProperty]
        public Category Category { get; set; }
        //ctor
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        // Th�m d? li?u
        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Th�m d? li?u th�nh c�ng";
            return RedirectToPage("Index");
        }
    }
}
