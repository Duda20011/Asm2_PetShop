using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using Repository;

namespace Asm2_PetShop.Pages.Login
{
    public class IndexModel : PageModel
    {
        public readonly IMemberRepository _memberRepository;
        public IndexModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        [BindProperty]
        public new PetShopMember Member { get; set; } = default!;
        public IActionResult OnPost()
        {
            if (Member.EmailAddress.IsNullOrEmpty() || Member.MemberPassword.IsNullOrEmpty())
            {
                ViewData["Message"] = "Please enter your email and password";
                return Page();
            }

            var member = _memberRepository.Login(Member.EmailAddress, Member.MemberPassword);

            if (member == null)
            {
                ViewData["Message"] = "Invalid email or password";
                return Page();
            }

            HttpContext.Session.SetString("membername", member.FullName);
            return RedirectToPage("/pet/Index");
        }
        
    }
}
