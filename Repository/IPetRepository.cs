using BussinessObject.Models;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPetRepository : IGenericRepository<Pet>
    {
        IEnumerable<Pet> Search(object filter);
        IEnumerable<Pet> Search(object filter, int pageNumber, int pageSize);
    }
}
