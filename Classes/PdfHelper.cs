using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Soc_Management_Web.Classes
{
    public class PdfHelper
    {
        public string CreateBlanckPdfFile(string filepath)
        {
            using (PdfWriter writer = new PdfWriter(filepath))
            {
                // Create a PdfDocument object
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Create a Document object
                    Document document = new Document(pdf);

                    // Add an empty paragraph to the document
                    document.Add(new Paragraph());

                    Console.WriteLine("Blank PDF file created at: " + filepath);
                }
            }
            File.Create(filepath).Dispose();
            return filepath;
        }
    }
}
