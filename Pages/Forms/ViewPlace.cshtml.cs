using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Forms
{
    public class ViewPlaceModel : PageModel
    {
        /*
        public ArchModel Place { get; set; }
        public IFormFile PhotoFile { get; set; }

        public void OnGet()
        {
        }
        */

        public Models.ArchModel Place { get; set; }

        private readonly WebApp.Data.ArchDbContext _context;
        public ViewPlaceModel(WebApp.Data.ArchDbContext context)
        {
            _context = context;
        }

        public Models.ArchModel ShowPlace { get; set; }

        public IActionResult OnGet(int id)
        {
            ShowPlace = _context.archModels.Find(id);
            return null;
        }
    }
}
