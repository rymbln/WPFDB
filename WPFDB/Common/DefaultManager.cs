using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Common
{
    public class DefaultManager
    {
        private static DefaultManager instance;
        private Conference defaultConference;

        public static DefaultManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultManager();
                    var conferenceList = DataManager.Instance.GetAllConferences();
                    instance.DefaultConference = conferenceList.FirstOrDefault(o => o.Name == "-");
                }
                return instance;
            }
        }

        public Conference DefaultConference
        {
            get { return this.defaultConference; }
            set { this.defaultConference = value; }
        }

    }
}
