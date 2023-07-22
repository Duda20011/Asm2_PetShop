using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.pet
{
    public class DeleteModel : PageModel
    {
        private readonly IPetRepository _petRepository;

        public DeleteModel(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [BindProperty]
      public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _petRepository.GetAll() == null)
            {
                return NotFound();
            }

            var pet = _petRepository.GetById(id);

            if (pet == null)
            {
                return NotFound();
            }
            else 
            {
                Pet = pet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _petRepository.GetAll() == null)
            {
                return NotFound();
            }
            var pet = _petRepository.GetById(id);

            if (pet != null)
            {
                Pet = pet;
                _petRepository.Delete(Pet);
                _petRepository.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
