using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.pet
{
    public class EditModel : PageModel
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetGroupRepository _petgroupRepository;


        public EditModel(IPetRepository petRepository, IPetGroupRepository petgroupRepository)
        {
            _petRepository = petRepository;
            _petgroupRepository = petgroupRepository;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _petRepository.GetAll() == null)
            {
                return NotFound();
            }

            var pet =  _petRepository.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            Pet = pet;
           ViewData["PetGroupId"] = new SelectList(_petgroupRepository.GetAll(), "PetGroupId", "PetGroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _petRepository.Update(Pet);
                _petRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(Pet.PetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PetExists(int id)
        {
          return (_petRepository.GetAll()?.Any(e => e.PetId == id)).GetValueOrDefault();
        }
    }
}
