using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ScienceDegreeRepository: IScienceDegreeRepository
    {
        private IObjectSet<ScienceDegree> objectSet;

        public ScienceDegreeRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.ScienceDegrees;
        }

        public IEnumerable<ScienceDegree> GetAllScienceDegrees()
        {
            return this.objectSet.ToList();
        }
    }
}
