using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.ViewModel;

namespace WPFDB.Common
{
    public static class PdfManager
    {

       

        public static void BeginBox(XGraphics gfx, int number, string title, 
            double borderWidth, double borderHeight,
            XColor shadowColor, XColor backColor, XColor backColor2,
            XPen borderPen)
        {
            const int dEllipse = 15;
            XRect rect = new XRect(0, 20, 300, 200);
            if (number % 2 == 0)
                rect.X = 300 - 5;
            rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
            rect.Inflate(-10, -10);
            XRect rect2 = rect;
            rect2.Offset(borderWidth,borderHeight);
            gfx.DrawRoundedRectangle(new XSolidBrush(shadowColor), rect2, new XSize(dEllipse + 8, dEllipse + 8));
            XLinearGradientBrush brush = new XLinearGradientBrush(rect, backColor, backColor2, XLinearGradientMode.Vertical);
            gfx.DrawRoundedRectangle(borderPen, brush, rect, new XSize(dEllipse, dEllipse));
            rect.Inflate(-5, -5);

            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            gfx.DrawString(title, font, XBrushes.Navy, rect, XStringFormats.TopCenter);

            rect.Inflate(-10, -5);
            rect.Y += 20;
            rect.Height -= 20;

            state = gfx.Save();
            gfx.TranslateTransform(rect.X, rect.Y);
        }

        public static void EndBox(XGraphics gfx)
        {
            gfx.Restore(state);
        }


        private static void DrawRoundedRectangle(XGraphics gfx, int number)
        {

       //     BeginBox(gfx, number, "DrawRoundedRectangle",3,5,
                
            XPen pen = new XPen(XColors.RoyalBlue, Math.PI);
            gfx.DrawRoundedRectangle(pen, 10, 0, 100, 60, 30, 20);
            gfx.DrawRoundedRectangle(XBrushes.Orange, 130, 0, 100, 60, 30, 20);
            gfx.DrawRoundedRectangle(pen, XBrushes.Orange, 10, 80, 100, 60, 30, 20);
            gfx.DrawRoundedRectangle(pen, XBrushes.Orange, 150, 80, 60, 60, 20, 20);

            EndBox(gfx);
        }

        private static void DrawRectangle(XGraphics gfx, int number)
        {
            var shadowColor = new XColor{ R = 25, G = 25, B = 25};
            var backColor = new XColor{ R = 50, G = 50, B = 50};
            var backColor2 = new XColor{ R = 150, G = 150, B = 150};
            var borderPen = new XPen(XColor.FromArgb(2568), 2);
            BeginBox(gfx, number, "DrawRectangle",10,20,shadowColor, backColor, backColor2, borderPen);

            XPen pen = new XPen(XColors.Navy, Math.PI);

            gfx.DrawRectangle(pen, 10, 0, 100, 60);
            gfx.DrawRectangle(XBrushes.DarkOrange, 130, 0, 100, 60);
            gfx.DrawRectangle(pen, XBrushes.DarkOrange, 10, 80, 100, 60);
            gfx.DrawRectangle(pen, XBrushes.DarkOrange, 150, 80, 60, 60);

            EndBox(gfx);
        }

         public static void DrawEllipse(XGraphics gfx, int number)
        {
   //         BeginBox(gfx, number, "DrawEllipse");

            XPen pen = new XPen(XColors.DarkBlue, 2.5);

            gfx.DrawEllipse(pen, 10, 0, 100, 60);
            gfx.DrawEllipse(XBrushes.Goldenrod, 130, 0, 100, 60);
            gfx.DrawEllipse(pen, XBrushes.Goldenrod, 10, 80, 100, 60);
            gfx.DrawEllipse(pen, XBrushes.Goldenrod, 150, 80, 60, 60);

            EndBox(gfx);
        }


        private static void DrawText(XGraphics gfx, int number)
        {
     //       BeginBox(gfx, number, "Text Styles");

            const string facename = "Times New Roman";

            //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);

            XFont fontRegular = new XFont(facename, 20, XFontStyle.Regular, options);
            XFont fontBold = new XFont(facename, 20, XFontStyle.Bold, options);
            XFont fontItalic = new XFont(facename, 20, XFontStyle.Italic, options);
            XFont fontBoldItalic = new XFont(facename, 20, XFontStyle.BoldItalic, options);

            // The default alignment is baseline left (that differs from GDI+)
            gfx.DrawString("Times (regular)", fontRegular, XBrushes.DarkSlateGray, 0, 30);
            gfx.DrawString("Times (bold)", fontBold, XBrushes.DarkSlateGray, 0, 65);
            gfx.DrawString("Times (italic)", fontItalic, XBrushes.DarkSlateGray, 0, 100);
            gfx.DrawString("Times (bold italic)", fontBoldItalic, XBrushes.DarkSlateGray, 0, 135);

            EndBox(gfx);
        }

        private static void DrawTextAlignment(XGraphics gfx, int number)
        {
    //        BeginBox(gfx, number, "Text Alignment");
            XRect rect = new XRect(0, 0, 250, 140);

            XFont font = new XFont("Verdana", 10);
            XBrush brush = XBrushes.Purple;
            XStringFormat format = new XStringFormat();

            gfx.DrawRectangle(XPens.YellowGreen, rect);
            gfx.DrawLine(XPens.YellowGreen, rect.Width / 2, 0, rect.Width / 2, rect.Height);
            gfx.DrawLine(XPens.YellowGreen, 0, rect.Height / 2, rect.Width, rect.Height / 2);

            gfx.DrawString("TopLeft", font, brush, rect, format);

            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("TopCenter", font, brush, rect, format);

            format.Alignment = XStringAlignment.Far;
            gfx.DrawString("TopRight", font, brush, rect, format);

            format.LineAlignment = XLineAlignment.Center;
            format.Alignment = XStringAlignment.Near;
            gfx.DrawString("CenterLeft", font, brush, rect, format);

            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("Center", font, brush, rect, format);

            format.Alignment = XStringAlignment.Far;
            gfx.DrawString("CenterRight", font, brush, rect, format);

            format.LineAlignment = XLineAlignment.Far;
            format.Alignment = XStringAlignment.Near;
            gfx.DrawString("BottomLeft", font, brush, rect, format);

            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("BottomCenter", font, brush, rect, format);

            format.Alignment = XStringAlignment.Far;
            gfx.DrawString("BottomRight", font, brush, rect, format);

            EndBox(gfx);
        }

        private static void DrawTitle(PdfPage page, XGraphics gfx, string title, PdfDocument doc)
        {
            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(-10, -15);
            XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
            gfx.DrawString(title, font, XBrushes.MidnightBlue, rect, XStringFormats.TopCenter);

            rect.Offset(0, 5);
            font = new XFont("Verdana", 8, XFontStyle.Italic);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Far;
            gfx.DrawString("Created with " + PdfSharp.ProductVersionInfo.Producer, font, XBrushes.DarkOrchid, rect, format);

            font = new XFont("Verdana", 8);
            format.Alignment = XStringAlignment.Center;
            gfx.DrawString(doc.PageCount.ToString(), font, XBrushes.DarkOrchid, rect, format);

            doc.Outlines.Add(title, page, true);
        }



        private static void DrawBadge(BadgeViewModel obj)
        {

            // Create a temporary file
            string filename = String.Format("{0}_tempfile.pdf", Guid.NewGuid().ToString("D").ToUpper());
            var s_document = new PdfDocument();
            s_document.Info.Title = "PDFsharp XGraphic Sample";
            s_document.Info.Author = "Stefan Lange";
            s_document.Info.Subject = "Created with code snippets that show the use of graphical functions";
            s_document.Info.Keywords = "PDFsharp, XGraphics";

            // Create  pages
            var page = s_document.AddPage();

            
            XGraphics gfx = XGraphics.FromPdfPage(page);

        

            DrawRectangle(gfx, 1);
         //   DrawRoundedRectangle(gfx, 2);
          //  DrawEllipse(gfx, 3);
        

            // Save the s_document...
            s_document.Save(filename);
            // ...and start a viewer
            Process.Start(filename);

        }

        public static  void Main(BadgeViewModel obj)
        {
            DrawBadge(obj);
        }


        public static XGraphicsState state { get; set; }
    }
}
