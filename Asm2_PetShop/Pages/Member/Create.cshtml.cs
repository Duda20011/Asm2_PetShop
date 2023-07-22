using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.Member
{
    public class CreateModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        public CreateModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PetShopMember PetShopMember { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _memberRepository.GetAll() == null || PetShopMember == null)
            {
                return Page();
            }

            _memberRepository.Insert(PetShopMember);
            _memberRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
