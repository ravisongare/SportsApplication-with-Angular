using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models.Repository
{
   public interface IUnitOfWork
    {
         IData data { get; set; }
        void commit();
    }
}
