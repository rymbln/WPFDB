using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDB.View
{
    /// <summary>
    /// Interaction logic for BadgeElementView.xaml
    /// </summary>
    public partial class BadgeElementView : UserControl
    {
        public BadgeElementView()
        {
            InitializeComponent();
        }

        private void TextBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var txt = sender as TextBox;

            if (txt == null)
                return;

            int txtvalue;
            if (int.TryParse(txt.Text, out txtvalue))
            {
                txt.Text = (txtvalue + e.Delta / 120).ToString();
      
            }
        }
    }
}
