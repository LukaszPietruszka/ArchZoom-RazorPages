using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Forms
{
    public class EditModel : PageModel
    {

        private readonly WebApp.Data.ArchDbContext _context;
        private readonly IWebHostEnvironment _hostenvironment;
        public EditModel(WebApp.Data.ArchDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostenvironment = webHostEnvironment;
        }

        [BindProperty]
        public ArchModel EditPlace { get; set; }

        [BindProperty]
        public FileViewModel FileUpload { get; set; }


        public IActionResult OnGet(int id)
        {
            EditPlace = _context.archModels.Find(id);
            _context.SaveChanges();
            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //upload pliku do folderu
            if (FileUpload.PhotoFile != null)
            {
                using (var stream = new FileStream(Path.Combine(_hostenvironment.WebRootPath, "PngDB", FileUpload.PhotoFile.FileName), FileMode.Create))
                {
                    await FileUpload.PhotoFile.CopyToAsync(stream);
                }
            }

            //zapisanie nazwy pliku
            using (var memoryStream = new MemoryStream())
            {
                if (FileUpload.PhotoFile != null)
                {
                    await FileUpload.PhotoFile.CopyToAsync(memoryStream);
                }

                //upload pliku ponizej 10 MB
                if (memoryStream.Length < 10485760)
                {
                    if (FileUpload.PhotoFile != null) //jeœli zdjêcie jest zmienione
                    {
                        EditPlace.Photo = FileUpload.PhotoFile.FileName;
                        _context.archModels.Update(EditPlace);
                    }
                    else   //jeœli zdjêcie NIE jest zmienione
                    {
                        _context.archModels.Update(EditPlace);
                    }

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
