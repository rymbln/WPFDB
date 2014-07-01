using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonConferenceWorkspaceViewModel: ViewModelBase
    {
        private PersonConferenceDetailViewModel registration;
        private PersonConferencePaymentViewModel payment;

        public PersonConference Model { get; private set; }

        public PersonConferenceWorkspaceViewModel(PersonConference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;
        }

        public PersonConferenceDetailViewModel Registration
        {
            get
            {
                if (this.Model.PersonConferences_Detail == null)
                {
                    this.Model.PersonConferences_Detail = new PersonConferences_Detail();
                }

                this.registration = new PersonConferenceDetailViewModel(this.Model.PersonConferences_Detail);

                return this.registration;
            }
        }

        public PersonConferencePaymentViewModel Payment
        {
            get
            {
                if (this.Model.PersonConferences_Payment == null)
                {
                    this.Model.PersonConferences_Payment = new PersonConferences_Payment();
                }
                this.payment = new PersonConferencePaymentViewModel(this.Model.PersonConferences_Payment);
                return this.payment;
            }
        }
    }
}
