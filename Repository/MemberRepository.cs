using BussinessObject.Models;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MemberRepository : GenericRepository<PetShopMember> , IMemberRepository
    {
        public PetShopMember? Login(string email, string password)
        {
            return GetAll().FirstOrDefault(x => x.EmailAddress == email && x.MemberPassword == password);
        }
        public void Register(PetShopMember member)
        {
            if (GetByEmail(member.EmailAddress) != null)
                throw new Exception("Email already exists");

            if (GetById(member.MemberId) != null)
                throw new Exception("Account already exists");

            member.MemberRole = 2;
            Insert(member);
        }
        public IEnumerable<PetShopMember> Search(object searchQuery)
        {
            return GetAll().Where(i => i.FullName.Contains(searchQuery.ToString()));
        }

        public PetShopMember GetByEmail(string email)
        {
            return GetAll().Where(i => i.EmailAddress == email).FirstOrDefault();
        }
    }
}
