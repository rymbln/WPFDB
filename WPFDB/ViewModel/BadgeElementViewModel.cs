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
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

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
                string[] figure = { "Arial", "Times New Roman", "Tahoma", "Verdana" };
                return figure.ToList();
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
            set { Model.PositionX1 = value; OnPropertyChanged("PositionX1"); }
        }
        public int PositionX2
        {
            get { return Model.PositionX2; }
            set { Model.PositionX2 = value; OnPropertyChanged("PositionX2"); }
        }
        public int PositionX3
        {
            get { return Model.PositionX3; }
            set { Model.PositionX3 = value; OnPropertyChanged("PositionX3"); }
        }
        public int PositionX4
        {
            get { return Model.PositionX4; }
            set { Model.PositionX4 = value; OnPropertyChanged("PositionX4"); }
        }
        public int PositionY1
        {
            get { return Model.PositionY1; }
            set { Model.PositionY1 = value; OnPropertyChanged("PositionY1"); }
        }
        public int PositionY2
        {
            get { return Model.PositionY2; }
            set { Model.PositionY2 = value; OnPropertyChanged("PositionY2"); }
        }
        public int PositionY3
        {
            get { return Model.PositionY3; }
            set { Model.PositionY3 = value; OnPropertyChanged("PositionY3"); }
        }
        public int PositionY4
        {
            get { return Model.PositionY4; }
            set { Model.PositionY4 = value; OnPropertyChanged("PositionY4"); }
        }
        public int Width
        {
            get { return Model.Width; }
            set { Model.Width = value;
            OnPropertyChanged("Width");
            }
        }
        public int Height
        {
            get { return Model.Height;}
            set { Model.Height = value;
            OnPropertyChanged("Height");
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
        public string PointsList
        {
            get { return String.Format("{0},{1} {2},{3} {4},{5} {6},{7}",PositionX1,PositionY1,PositionX2, PositionY2, PositionX3, PositionY3, PositionX4, PositionY4); }
        }
        public Color ColorFontForeground
        {
            get { return (Color)ColorConverter.ConvertFromString(FontColor); }
        }
        public Color ColorFill
        {
            get { return (Color)ColorConverter.ConvertFromString(BackgroundColor); }
        }
        public Color ColorStroke
        {
            get { return (Color)ColorConverter.ConvertFromString(ForegroundColor); }
        }


    }
}
