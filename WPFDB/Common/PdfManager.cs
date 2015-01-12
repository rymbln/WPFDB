using MigraDoc.DocumentObjectModel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;
using WPFDB.ViewModel;

namespace WPFDB.Common
{
    public enum PdfMode
    {
        BADGE, ORDER

    }

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
            rect2.Offset(borderWidth, borderHeight);
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

        private static void DrawRectangle(XGraphics gfx,
            XColor backColor, XColor borderColor,
            XPen borderPen,
            int width, int height,
            int x, int y)
        {

            gfx.DrawRectangle(borderPen, x, y, width, height);
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


        private static void DrawText(XGraphics gfx, string text, string font, string style, double size, string color, int width, int height, int x, int y)
        {
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontStyle = null;
            switch (style)
            {
                case "Bold":
                    fontStyle = new XFont(font, size, XFontStyle.Bold, options);
                    break;
                case "Italic":
                    fontStyle = new XFont(font, size, XFontStyle.Italic, options);
                    break;
                case "Regular":
                    fontStyle = new XFont(font, size, XFontStyle.Regular, options);
                    break;
                case "Underline":
                    fontStyle = new XFont(font, size, XFontStyle.Underline, options);
                    break;
                case "Strikeout":
                    fontStyle = new XFont(font, size, XFontStyle.Strikeout, options);
                    break;
                case "BoldItalic":
                    fontStyle = new XFont(font, size, XFontStyle.BoldItalic, options);
                    break;
                default:
                    break;
            }
            System.Drawing.Color fontColor = ConverterManager.HexToColorConverter(color);
            XBrush fontBrush = new XSolidBrush(new XColor { R = fontColor.R, G = fontColor.G, B = fontColor.B });
            XRect fontRect = new XRect(x, y, width, height);
            XStringFormat fontFormat = new XStringFormat { Alignment = XStringAlignment.Center };
            gfx.DrawString(text, fontStyle, fontBrush, fontRect, fontFormat);
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



        private static string DrawBadge(BadgeType obj, Person person)
        {
            string filename;
            // Create a temporary file

            var s_document = new PdfDocument();
            if (person != null)
            {
                 filename = String.Format("{0}_{1}_{2}.pdf", person.Id, person.FullName, DefaultManager.Instance.CurrentDateTimeShortString);
                 s_document.Info.Title = person.FullName;
            } else
            {
                 filename = String.Format("{0}.pdf",  DefaultManager.Instance.CurrentDateTimeShortString);
                 s_document.Info.Title = "IACMAC";
            }
                       
            s_document.Info.Author = "IACMAC";
            s_document.Info.Subject = "IACMAC Conference Badge";

            // Create  pages
            var page = s_document.AddPage();

            PageSize[] pageSizes = (PageSize[])Enum.GetValues(typeof(PageSize));
            foreach (PageSize pageSize in pageSizes)
            {
                if (pageSize == PageSize.Undefined)
                    continue;
                page.Width = obj.Width;
                page.Height = obj.Height;
                page.Orientation = PageOrientation.Portrait;
            }

            XGraphics gfx = XGraphics.FromPdfPage(page);


            foreach (var badge in obj.Badges.ToList())
            {
                DrawBadgeElement(badge, gfx);
            }

            //   DrawRoundedRectangle(gfx, 2);
            //  DrawEllipse(gfx, 3);


            // Save the s_document...
            s_document.Save(filename);
            // ...and start a viewer
            return filename;

        }

        private static void DrawBadgeElement(Badge badge, XGraphics gfx)
        {
            var color = ConverterManager.HexToColorConverter(badge.ForegroundColor);
            XColor borderColor = new XColor { R = color.R, G = color.G, B = color.B };
            color = ConverterManager.HexToColorConverter(badge.BackgroundColor);
            XColor backColor = new XColor { R = color.R, G = color.G, B = color.B };
            color = ConverterManager.HexToColorConverter(badge.FontColor);
            XColor fontColor = new XColor { R = color.R, G = color.G, B = color.B };
            XPen pen = new XPen(borderColor, double.Parse(badge.BorderWidth.ToString()));


            DrawRectangle(gfx, backColor, borderColor, pen, badge.Width, badge.Height, badge.PositionX1, badge.PositionY1);
            DrawText(gfx, badge.Value, badge.Font, badge.FontStyle, badge.FontSize, badge.FontColor, badge.Width, badge.Height, badge.PositionX1, badge.PositionY1);
        }

        public static string Generate(PdfMode mode, BadgeType obj, Person person)
        {
            switch (mode)
            {
                case PdfMode.BADGE:
                    return DrawBadge(obj, person);

                case PdfMode.ORDER:
                    return "";
                default:
                    return "";
            }

        }


        public static XGraphicsState state { get; set; }
    }
}
