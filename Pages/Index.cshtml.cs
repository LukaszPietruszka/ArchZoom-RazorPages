using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.ArchDbContext _context;
        public IndexModel(WebApp.Data.ArchDbContext context)
        {
            _context = context;
        }

        public IList<ArchModel> Place { get; set; } //lista do foreach - wczytywanie postow

        public async Task OnGetAsync()
        {
            Place = await _context.archModels.ToListAsync(); //wczytanie postow do listy
        }
    }
}
