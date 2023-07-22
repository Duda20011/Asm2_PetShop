using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.petgroup
{
    public class CreateModel : PageModel
    {
        private readonly IPetGroupRepository _petgroupRepository;

        public CreateModel(IPetGroupRepository petgroupRepository)
        {
            _petgroupRepository = petgroupRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PetGroup PetGroup { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _petgroupRepository.GetAll() == null || PetGroup == null)
            {
                return Page();
            }

            _petgroupRepository.Insert(PetGroup);
            _petgroupRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
