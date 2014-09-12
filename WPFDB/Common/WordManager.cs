using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDB.Model;
using Word = Microsoft.Office.Interop.Word;

namespace WPFDB.Common
{
    public static class WordManager
    {
        private static object obj;
        private static Word.Application objWord;
        private static Word.Application CreateWordObj()
        {

            try
            {
                objWord = new Word.Application();
                objWord.Visible = false;
                obj = objWord;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка создания экземпляра MS Word");
            }
            return (obj as Word.Application);
        }

        public static string AbstractToWord(Abstract abs)
        {
            object missing = System.Reflection.Missing.Value;
            int rowsCount;
            int colsCount;
            var filePath = "---";
            try
            {
                filePath = DefaultManager.Instance.AbstractFilePath;

                var filename = "Тезисы_" + abs.SourceId.ToString() + "_" + DateTime.Now.ToShortDateString() + ".docx";

                var objWord = CreateWordObj();

                var doc = objWord.Documents.Add();
                doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

                Word.Paragraph paragraphBefore = doc.Paragraphs[1];
                Word.Range rngBefore1 = paragraphBefore.Range;


                /*======================================================================*/
                /* How to insert <Current Page> of <Total Pages> into the footer for a MS Word Document in VSTO*/

                // Open up the footer in the word document

                objWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;


                // Set current Paragraph Alignment to Center
                objWord.ActiveWindow.ActivePane.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                // Type in 'Page '
                objWord.ActiveWindow.Selection.TypeText("Страница ");

                // Add in current page field
                Object CurrentPage = Word.WdFieldType.wdFieldPage;
                objWord.ActiveWindow.Selection.Fields.Add(objWord.ActiveWindow.Selection.Range, ref CurrentPage, ref missing, ref missing);

                // Type in ' of '
                objWord.ActiveWindow.Selection.TypeText(" из ");

                // Add in total page field
                Object TotalPages = Word.WdFieldType.wdFieldNumPages;
                objWord.ActiveWindow.Selection.Fields.Add(objWord.ActiveWindow.Selection.Range, ref TotalPages, ref missing, ref missing);
                /*======================================================================*/

                //Add header
                foreach (Word.Section section in doc.Sections)
                {
                    //Get the header range and add the header details.
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Word.WdColorIndex.wdGray50;
                    headerRange.Font.Size = 10;
                    headerRange.Text = DefaultManager.Instance.DefaultConference.Name + "\r\n" + DateTime.Now;
                }
             
                object styleHeading1 = Word.WdBuiltinStyle.wdStyleHeading1;
                object styleHeading2 = Word.WdBuiltinStyle.wdStyleHeading2;
                object styleNormal = Word.WdBuiltinStyle.wdStyleNormal;

                //Add paragraph with Heading 1 style
                Word.Paragraph para1 = doc.Content.Paragraphs.Add();
                
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = "Тезис " + abs.SourceId;
                para1.Range.InsertParagraphAfter();

                
                Word.Paragraph para2 = doc.Content.Paragraphs.Add();
                para2.Range.set_Style(ref styleHeading2);
                para2.set_Style(ref styleHeading2);
                para2.Range.Text = "Автор";
                para2.Range.InsertParagraphAfter();

                Word.Paragraph para3 = doc.Content.Paragraphs.Add();
                para3.Range.set_Style(ref styleNormal);
                para3.Range.Text = abs.Author.FullName;
                para3.Range.InsertParagraphAfter();

                Word.Paragraph para4= doc.Content.Paragraphs.Add();
                para4.Range.set_Style(ref styleHeading2);
                para4.set_Style(ref styleHeading2);
                para4.Range.Text = "Место работы автора";
                para4.Range.InsertParagraphAfter();

                Word.Paragraph para5 = doc.Content.Paragraphs.Add();
                para5.Range.set_Style(ref styleNormal);
                para5.Range.Text = abs.Author.WorkPlace;
                para5.Range.InsertParagraphAfter();

                Word.Paragraph para6 = doc.Content.Paragraphs.Add();
                para6.Range.set_Style(ref styleHeading2);
                para6.set_Style(ref styleHeading2);
                para6.Range.Text = "Соавторы";
                para6.Range.InsertParagraphAfter();

                Word.Paragraph para7 = doc.Content.Paragraphs.Add();
                para7.Range.set_Style(ref styleNormal);
                para7.Range.Text = abs.OtherAuthors;
                para7.Range.InsertParagraphAfter();

                Word.Paragraph para8 = doc.Content.Paragraphs.Add();
                para8.Range.set_Style(ref styleHeading2);
                para8.set_Style(ref styleHeading2);
                para8.Range.Text = "Название";
                para8.Range.InsertParagraphAfter();

                Word.Paragraph para9 = doc.Content.Paragraphs.Add();
                para9.Range.set_Style(ref styleNormal);
                para9.Range.Text = abs.Name;
                para9.Range.InsertParagraphAfter();

                Word.Paragraph para10 = doc.Content.Paragraphs.Add();
                para10.Range.set_Style(ref styleHeading2);
                para10.set_Style(ref styleHeading2);
                para10.Range.Text = "Текст";
                para10.Range.InsertParagraphAfter();

                Word.Paragraph para11 = doc.Content.Paragraphs.Add();
                para11.Range.set_Style(ref styleNormal);
                para11.Range.Text = abs.Text;
                para11.Range.InsertParagraphAfter();

                //Save the document
                doc.SaveAs(filePath + "\\" + filename);
                doc.Close();
                doc = null;
                objWord.Quit();
                objWord = null;
                obj = null;
        //        MessageBox.Show("Document created successfully !");


                return filePath + "\\" + filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace);
                return null;
            }
            finally
            {

                GC.Collect();

            }
        }
    }
}
