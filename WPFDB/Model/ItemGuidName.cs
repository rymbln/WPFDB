using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Model
{
   public class ItemGuidName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemGuidName()
        {
            Id = new Guid();
            Name = "---";
        }
        public ItemGuidName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
