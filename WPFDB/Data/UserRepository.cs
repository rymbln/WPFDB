using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class UserRepository
    {
              private IObjectSet<User> objectSet;

        public UserRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.Users;
        }

        public IEnumerable<User> All()
        {
            return objectSet.ToList();
        }

        public void Add(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }

        public User GetUser(string name)
        {
            return objectSet.FirstOrDefault(o => o.Name == name);
        }

        public User GetUser(string name, string pass)
        {
            return objectSet.FirstOrDefault(o => o.Name == name && o.Password == pass);
        }

        public IEnumerable<User> GetUsers()
        {
            return objectSet.ToList();
        }
    }
}
