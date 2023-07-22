using BussinessObject.Models;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMemberRepository : IGenericRepository<PetShopMember>
    {
        PetShopMember Login(string email, string password);
        void Register(PetShopMember member);
        IEnumerable<PetShopMember> Search(object searchQuery);
        PetShopMember GetByEmail(string email);
    }
}
