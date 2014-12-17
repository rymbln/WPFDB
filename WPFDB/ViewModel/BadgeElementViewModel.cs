using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing.Text;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;
using System.Drawing;

namespace WPFDB.ViewModel
{
    public class BadgeElementViewModel : ViewModelBase
    {
        public static int Errors { get; set; }
        public Badge Model { get; private set; }
 

        public BadgeElementViewModel(Badge obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Badge");
            }
            this.Model = obj;
    
        }


        public List<string> FontLookup
        {
            get
            {
                string familyName;
                string familyList = "";
                System.Drawing.FontFamily[] fontFamilies;

                InstalledFontCollection installedFontCollection = new InstalledFontCollection();

                // Get the array of FontFamily objects.
                fontFamilies = installedFontCollection.Families;

                var fonts = new List<string>();
                int count = fontFamilies.Length;
                for (int j = 0; j < count; ++j)
                {
                    fonts.Add(fontFamilies[j].Name);
                }
                return fonts;
            }
        }

        public List<string> FontStyleLookup
        {
            get
            {
                string[] figure = { "Bold", "Italic", "Regular","Strikeout", "Underline" };
                return figure.ToList();
            }
        }
      


    
        public List<string> TextSamplesLookup
        {
            get
            {
                string[] textes = { "$F", "$FI", "$FIO", "$POST", "$CITY", "$COUNTRY", "$COMPANY" };
                return textes.ToList();
            }
        }

        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set { }
        }
        public int SourceId
        {
            get
            {
                return this.Model.SourceId;
            }

            set
            {
                this.Model.SourceId = value;
                this.OnPropertyChanged("SourceId");
            }
        }
        public string Name
        {
            get { return this.Model.Name; }
            set { this.Model.Name = value; this.OnPropertyChanged("Name"); }
        }
        public string Font
        {
            get { return this.Model.Font; }
            set { this.Model.Font = value; this.OnPropertyChanged("Font"); }
        }
        public string FontStyle
        {
            get { return this.Model.FontStyle; }
            set { this.Model.FontStyle = value; this.OnPropertyChanged("FontStyle"); }
        }
        public string Value
        {
            get { return this.Model.Value; }
            set { this.Model.Value = value; this.OnPropertyChanged("Value"); }
        }
        public int PositionX1
        {
            get { return Model.PositionX1; }
            set { Model.PositionX1 = value; OnPropertyChanged("PositionX1");
            OnPropertyChanged("MarginValue");
            }
        }
     
        public int PositionY1
        {
            get { return Model.PositionY1; }
            set { Model.PositionY1 = value; 
                OnPropertyChanged("PositionY1");
                OnPropertyChanged("MarginValue");
            }
        }
    
        public int Width
        {
            get { return Model.Width; }
            set { Model.Width = value;
            OnPropertyChanged("Width");
            OnPropertyChanged("MarginValue");
            }
        }
        public int Height
        {
            get { return Model.Height;}
            set { Model.Height = value;
            OnPropertyChanged("Height");
            OnPropertyChanged("MarginValue");
            }
        }
        public int RoundCorner
        {
            get { return Model.RoundCorner; }
            set { Model.RoundCorner = value; OnPropertyChanged("RoundCorner"); }
        }
        public string ForegroundColor
        {
            get { return this.Model.ForegroundColor; }
            set { this.Model.ForegroundColor = value; this.OnPropertyChanged("ForegroundColor"); }
        }
        public string BackgroundColor
        {
            get { return this.Model.BackgroundColor ; }
            set { this.Model.BackgroundColor = value; this.OnPropertyChanged("BackgroundColor"); }
        }
       
        public string FontColor
        {
            get { return this.Model.FontColor ; }
            set { this.Model.FontColor = value; this.OnPropertyChanged("FontColor"); }
        }
        public int FontSize
        {
            get { return this.Model.FontSize; }
            set { this.Model.FontSize = value; this.OnPropertyChanged("FontSize"); }
        }
        public string FontInfo
        {
            get
            {
                return Font + "|" + this.FontStyle + "|" + FontSize;
            }
        }
           public string MarginValue
        {
            get
            {
                OnPropertyChanged("BadgeElementCollection"); 
                return String.Format("{0},{1},0,0", PositionX1, PositionY1); }
        }
    }
}
