using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ConferenceRepository: IConferenceRepository
    {
        private IObjectSet<Conference> objectSet;

        public ConferenceRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.Conferences;
        }

        public IEnumerable<Conference> GetAllConferences()
        {
            return objectSet.ToList();
        }

    }
}
