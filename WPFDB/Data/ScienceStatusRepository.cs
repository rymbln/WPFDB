using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ScienceStatusRepository: IScienceStatusRepository
    {
        private IObjectSet<ScienceStatus> objectSet;

        public ScienceStatusRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.ScienceStatuses;
        }

        public IEnumerable<ScienceStatus> GetAllScienceStatuses()
        {
            return this.objectSet.ToList();
        }
    }
}
