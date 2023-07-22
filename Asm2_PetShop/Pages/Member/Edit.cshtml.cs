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

namespace Asm2_PetShop.Pages.Member
{
    public class EditModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        public EditModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [BindProperty]
        public PetShopMember PetShopMember { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _memberRepository.GetAll() == null)
            {
                return NotFound();
            }

            var petshopmember = _memberRepository.GetById(id);
            if (petshopmember == null)
            {
                return NotFound();
            }
            PetShopMember = petshopmember;
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
                _memberRepository.Update(PetShopMember);
                _memberRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetShopMemberExists(PetShopMember.MemberId))
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

        private bool PetShopMemberExists(string id)
        {
          return (_memberRepository.GetAll()?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
