using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View.Common;
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

        public List<string> FigureLookup
        {
            get
            {
                string[] figure = { "Прямоугольник", "Прямоугольник скругленный", "Эллипс", "Треугольник" };
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
        public string Figure
        {
            get { return this.Model.Figure; }
            set { this.Model.Figure = value; this.OnPropertyChanged("Figure"); }
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
        public int PositionX
        {
            get { return Model.PositionX; }
            set { Model.PositionX = value; OnPropertyChanged("PositionX"); }
        }
        public int PositionY
        {
            get { return Model.PositionY; }
            set { Model.PositionY = value; OnPropertyChanged("PositionY"); }
        }
        public int Width
        {
            get { return this.Model.Width; }
            set { this.Model.Width = value; this.OnPropertyChanged("Width"); }
        }
        public int Height
        {
            get { return this.Model.Height; }
            set { this.Model.Height = value; this.OnPropertyChanged("Height"); }
        }
        public int ForegroundColor
        {
            get { return this.Model.ForegroundColor; }
            set { this.Model.ForegroundColor = value; this.OnPropertyChanged("ForegroundColor"); }
        }
        public int BackgroundColor
        {
            get { return this.Model.BackgroundColor ; }
            set { this.Model.BackgroundColor = value; this.OnPropertyChanged("BackgroundColor"); }
        }
        public int FontColor
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



    }
}
