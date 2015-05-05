using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Model
{
   public class ItemIntName
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public ItemIntName()
        {
            Id = 0;
            Name = "---";
        }
        public ItemIntName(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
