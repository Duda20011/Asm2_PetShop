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

namespace Asm2_PetShop.Pages.petgroup
{
    public class EditModel : PageModel
    {
        private readonly IPetGroupRepository _petgroupRepository;

        public EditModel(IPetGroupRepository petgroupRepository)
        {
            _petgroupRepository = petgroupRepository;
        }

        [BindProperty]
        public PetGroup PetGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _petgroupRepository.GetAll() == null)
            {
                return NotFound();
            }

            var petgroup =  _petgroupRepository.GetById(id);
            if (petgroup == null)
            {
                return NotFound();
            }
            PetGroup = petgroup;
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
                _petgroupRepository.Update(PetGroup);
                _petgroupRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetGroupExists(PetGroup.PetGroupId))
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

        private bool PetGroupExists(string id)
        {
          return (_petgroupRepository.GetAll()?.Any(e => e.PetGroupId == id)).GetValueOrDefault();
        }
    }
}
