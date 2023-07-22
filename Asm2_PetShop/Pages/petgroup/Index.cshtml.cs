using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repository;
using Microsoft.IdentityModel.Tokens;

namespace Asm2_PetShop.Pages.petgroup
{
    public class IndexModel : PageModel
    {
        private readonly IPetGroupRepository _petgroupRepository;

        public IndexModel(IPetGroupRepository petgroupRepository)
        {
            _petgroupRepository = petgroupRepository;
        }

        public IList<PetGroup> PetGroup { get;set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        private const int PageSize = 10;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            TotalPages = _petgroupRepository.GetTotalPages(PageSize);

            if (_petgroupRepository.GetAll() == null)
                return;

            if (pageNumber < 1 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;

            PetGroup = _petgroupRepository.GetAll(pageNumber, PageSize).ToList();
        }
        public void OnPostSearch(string searchQuery, int pageNumber = 1)
        {
            if (!searchQuery.IsNullOrEmpty())
            {
                PetGroup = _petgroupRepository.Search(searchQuery).ToList();
            }
            else
            {
                TotalPages = _petgroupRepository.GetTotalPages(PageSize);
                PetGroup = _petgroupRepository.GetAll(pageNumber, PageSize).ToList();
            }
        }
    }
}
