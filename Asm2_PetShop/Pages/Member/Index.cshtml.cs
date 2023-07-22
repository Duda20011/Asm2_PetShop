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

namespace Asm2_PetShop.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        public IndexModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IList<PetShopMember> PetShopMember { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        private const int PageSize = 10;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            TotalPages = _memberRepository.GetTotalPages(PageSize);

            if (_memberRepository.GetAll() == null)
                return;

            if (pageNumber < 1 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;

            PetShopMember = _memberRepository.GetAll(pageNumber, PageSize).ToList();
        }
        public void OnPostSearch(string searchQuery, int pageNumber = 1)
        {
            if (!searchQuery.IsNullOrEmpty())
            {
                PetShopMember = _memberRepository.Search(searchQuery).ToList();
            }
            else
            {
                TotalPages = _memberRepository.GetTotalPages(PageSize);
                PetShopMember = _memberRepository.GetAll(pageNumber, PageSize).ToList();
            }
        }
    }
}

