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

namespace Asm2_PetShop.Pages.pet
{
    public class IndexModel : PageModel
    {
        private readonly IPetRepository _petRepository;

        public IndexModel(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public IList<Pet> Pet { get;set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        private const int PageSize = 10;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            TotalPages = _petRepository.GetTotalPages(PageSize);

            if (_petRepository.GetAll() == null)
                return;

            if (pageNumber < 1 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;

            Pet = _petRepository.GetAll(pageNumber, PageSize).ToList();
        }
        public void OnPostSearch(string searchQuery, int pageNumber = 1)
        {
            if (!searchQuery.IsNullOrEmpty())
            {
                Pet = _petRepository.Search(searchQuery).ToList();
            }
            else
            {
                TotalPages = _petRepository.GetTotalPages(PageSize);
                Pet = _petRepository.GetAll(pageNumber, PageSize).ToList();
            }
        }
    }
}
