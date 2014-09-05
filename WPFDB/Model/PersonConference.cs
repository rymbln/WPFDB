using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Model
{
    public partial class PersonConference: EntityObject
    {
        public string ConferenceName
        {
            get { return this.Conference.Name; }
        }
    }
}
