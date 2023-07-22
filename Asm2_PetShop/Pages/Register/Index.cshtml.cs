using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Asm2_PetShop.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;

        public IndexModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [BindProperty] public new PetShopMember Member { get; set; } = default!;

        public IActionResult OnPost()
        {
            try
            {
                _memberRepository.Register(Member);
                _memberRepository.Save();
                return RedirectToPage("/login/Index");
            }
            catch (Exception e)
            {
                if (e.Message == "Email already exists")
                    ViewData["Message"] = "Email already exists";
                if (e.Message == "Account already exists")
                    ViewData["Message"] = "Account already exists";
                Console.WriteLine(e.Message);
            }
            return Page();
        }
    }
}
