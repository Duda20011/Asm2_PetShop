﻿using BussinessObject.Models;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPetGroupRepository : IGenericRepository<PetGroup>
    {
        IEnumerable<PetGroup> Search(object filter);
        IEnumerable<PetGroup> Search(object filter, int pageNumber, int pageSize);
    }
}
