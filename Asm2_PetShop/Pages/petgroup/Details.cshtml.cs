using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repository;

namespace Asm2_PetShop.Pages.petgroup
{
    public class DetailsModel : PageModel
    {
        private readonly IPetGroupRepository _petgroupRepository;

        public DetailsModel(IPetGroupRepository petgroupRepository)
        {
            _petgroupRepository = petgroupRepository;
        }

        public PetGroup PetGroup { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _petgroupRepository.GetAll() == null)
            {
                return NotFound();
            }

            var petgroup = _petgroupRepository.GetById(id);
            if (petgroup == null)
            {
                return NotFound();
            }
            else 
            {
                PetGroup = petgroup;
            }
            return Page();
        }
    }
}
