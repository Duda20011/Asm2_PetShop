using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.pet
{
    public class CreateModel : PageModel
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetGroupRepository _petgroupRepository;

        public CreateModel(IPetRepository petRepository, IPetGroupRepository petgroupRepository)
        {
            _petRepository = petRepository;
            _petgroupRepository = petgroupRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["PetGroupId"] = new SelectList(_petgroupRepository.GetAll(), "PetGroupId", "PetGroupName");
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _petRepository.GetAll() == null || Pet == null)
            {
                return Page();
            }

            _petRepository.Insert(Pet);
            _petRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
