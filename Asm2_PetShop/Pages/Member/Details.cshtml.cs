using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.Member
{
    public class DetailsModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        public DetailsModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

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
            else 
            {
                PetShopMember = petshopmember;
            }
            return Page();
        }
    }
}
