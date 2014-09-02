using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDB.Common
{
    static class LogManager
    {
        public static void Write(string str)
        {
            MessageBox.Show(DateTime.Now + ": " + str);
        }

        public static void Write(Exception ex)
        {
            MessageBox.Show(DateTime.Now + ": " + ex.Message + ", " + ex.Source + ", " + ex.StackTrace + ", " + ex.InnerException);
        }
    }
}
