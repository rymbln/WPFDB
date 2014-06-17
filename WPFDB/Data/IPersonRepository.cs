using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPersons();
    }
}
