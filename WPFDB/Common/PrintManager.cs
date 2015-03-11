using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDB.Common
{
    public static class PrintManager
    {

        private static System.Drawing.Image photo;

        private static string GetPrinter(DocumentType docType)
        {
            switch (docType)
            {
                case DocumentType.BADGE:
                    return DataManager.Instance.GetPrinter("BADGE");
                case DocumentType.ORDER:
                    return DataManager.Instance.GetPrinter("ORDER");
                default:
                    return DefaultManager.Instance.Printer;
            }

        }
       
        private static void PrintImage(string address, DocumentType docType)
        {
             photo = System.Drawing.Image.FromFile(address);
            var pd = new PrintDocument();
            pd.DocumentName = address;
            pd.PrintPage += pd_PrintPage;
            pd.PrinterSettings.PrinterName = GetPrinter(DocumentType.BADGE);

            PaperSize paperSize = new PaperSize("Conference Badge", photo.Width, photo.Height);
            paperSize.RawKind = (int)PaperKind.Custom;

            pd.PrintPage += (sender, args) => 
                Console.Out.WriteLine("Printable Area for printer {0} = {1}", 
                args.PageSettings.PrinterSettings.PrinterName, args.PageSettings.PrintableArea);

            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            Console.Out.WriteLine("My paper size: " + pd.DefaultPageSettings.PaperSize);
            pd.Print();
                   }

        static void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(photo, 0,0);
        }

        public static void Print(string address, DocumentType docType)
        {

            switch (docType)
            {
                case DocumentType.BADGE:
                    PrintImage(address, docType);
                    break;
                case DocumentType.ORDER:
                    break;
                default:
                    break;
            }

            //ProcessStartInfo info = new ProcessStartInfo();
            //info.Verb = "print";
            //info.FileName = address;
            //info.CreateNoWindow = true;
            //info.WindowStyle = ProcessWindowStyle.Hidden;

            //Process p = new Process();
            //p.StartInfo = info;
            //p.Start();

            //p.WaitForInputIdle();
            //System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();
        }
    }
}
