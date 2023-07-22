using BussinessObject.Models;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PetGroupRepository : GenericRepository<PetGroup>, IPetGroupRepository
    {
        public IEnumerable<PetGroup> Search(object filter)
        {
            return GetAll().Where(x => x.PetGroupName.Contains(filter.ToString()));
        }

        public IEnumerable<PetGroup> Search(object filter, int pageNumber, int pageSize)
        {
            return Search(filter).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
