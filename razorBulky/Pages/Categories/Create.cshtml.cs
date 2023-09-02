using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorBulky.Data;
using razorBulky.Models;

namespace razorBulky.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
       
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
                
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
            {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["Success"] = "Created";
            return RedirectToPage("Index");
        }

    }
}
