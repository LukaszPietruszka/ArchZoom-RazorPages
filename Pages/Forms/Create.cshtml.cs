using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApp.Pages.Forms
{
    public class CreateModel : PageModel
    {
        /*
            public ArchModel Place { get; set; }
            public IFormFile PhotoFile { get; set; }

        public void OnGet()
        {
        }
        */

        private readonly WebApp.Data.ArchDbContext _context;
        private readonly IWebHostEnvironment _hostenvironment;
        public CreateModel(WebApp.Data.ArchDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostenvironment = webHostEnvironment;
        }

        [BindProperty]
        public ArchModel Place { get; set; }

        [BindProperty]
        public FileViewModel FileUpload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //upload pliku do folderu
            if (FileUpload.PhotoFile.Length > 0)
            {
                using (var stream = new FileStream(Path.Combine(_hostenvironment.WebRootPath, "PngDB", FileUpload.PhotoFile.FileName), FileMode.Create))
                {
                    await FileUpload.PhotoFile.CopyToAsync(stream);
                }
            }

            //zapisanie nazwy pliku
            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.PhotoFile.CopyToAsync(memoryStream);

                //upload pliku ponizej 10 MB
                if (memoryStream.Length < 10485760)
                {
                    var post = new ArchModel() //wartosci z formularza
                    {
                        Name = Place.Name,
                        Country = Place.Country,
                        City = Place.City,
                        Description = Place.Description,
                        Photo = FileUpload.PhotoFile.FileName,
                    };
                    _context.archModels.Add(post);   //dodanie miejsca do bazy

                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("File", "Plik, który próbujesz dodaæ jest za du¿y.");
                }
            }
            return RedirectToPage("/Index");
        }

    }
}
