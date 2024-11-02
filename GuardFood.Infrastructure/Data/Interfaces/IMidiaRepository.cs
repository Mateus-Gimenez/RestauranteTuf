using GuardFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IMidiaRepository
    {
        Midia GetById(Guid Id);
        void InsertOrReplace(Midia restaurante);
        void Delete(Guid Id);
    }
}
