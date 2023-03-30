using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Forms
{
    public class DeleteModel : PageModel
    {
        public Models.ArchModel Place { get; set; }

        private readonly WebApp.Data.ArchDbContext _context;
        public DeleteModel(WebApp.Data.ArchDbContext context)
        {
            _context = context;
        }

        public Models.ArchModel DelPlace { get; set; }

        public IActionResult OnGet(int id)
        {
            DelPlace = _context.archModels.Find(id);
            _context.archModels.Remove(DelPlace);
            _context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
