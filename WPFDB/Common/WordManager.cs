using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Common
{
    public static class WordManager
    {
       
        public static string AbstractToWord(Abstract abs)
        {
           return abs.Link;
        }
    }
}
