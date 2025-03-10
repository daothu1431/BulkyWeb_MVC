using BullkeyWebRazor_Teamp.Data;
using BullkeyWebRazor_Teamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkeyWebRazor_Teamp.Pages.Categories
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        // prop
        public List<Category> CategoryList { get; set; }
        //ctor
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }

        
    }
}
