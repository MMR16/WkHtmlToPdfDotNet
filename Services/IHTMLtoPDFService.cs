namespace WkHtmlToPdf.Services
{
    public interface IHTMLtoPDFService
    {
        byte[] ConvertHTMLtoPDF(string htmlContext, string reportTitle, bool pageCount = true, bool portrait = true, bool headerLine = true);
    }

}
