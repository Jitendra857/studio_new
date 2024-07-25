using iTextSharp.text.pdf;
using iTextSharp.text;


namespace Soc_Management_Web.Common
{
    public class MyPageEventHandler: PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.TotalWidth = document.PageSize.Width;
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            string imagePath = @"http://studio.pioerp.com/TOP5.png";
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
            PdfPCell imgCell = new PdfPCell(img);
            imgCell.Colspan = 5;
            imgCell.HorizontalAlignment = Element.ALIGN_CENTER;
            imgCell.Border = 0;
            imgCell.PaddingTop = -55;
            headerTable.AddCell(imgCell);
            headerTable.WriteSelectedRows(0, -1, 0, document.PageSize.Height - document.TopMargin - 20, writer.DirectContent);

            // Create a table for the footer
            PdfPTable footerTable = new PdfPTable(1);
            footerTable.TotalWidth = document.PageSize.Width;
            footerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            string imagePath1 = @"http://studio.pioerp.com/BOTTOM_1.png";
            iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(imagePath1);
            PdfPCell imgCell1 = new PdfPCell(img1);
            imgCell1.Colspan = 5;
            imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            imgCell1.Border = 0;
            imgCell1.PaddingBottom = 60;
            footerTable.AddCell(imgCell1);

            // Write the footer table to the PDF content
            footerTable.WriteSelectedRows(0, -1, 0, document.BottomMargin + 60, writer.DirectContent);
        }
    }
}

