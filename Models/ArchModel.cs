using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ArchModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; } = string.Empty; 

        [Display(Name = "Państwo")]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Miasto")]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Opis")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; } = string.Empty;
    }

    public class FileViewModel
    {
        public IFormFile PhotoFile { get; set; }
    }

}
