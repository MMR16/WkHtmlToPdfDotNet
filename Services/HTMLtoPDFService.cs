using System.Text;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;

namespace WkHtmlToPdf.Services
{

    public class HTMLtoPDFService : IHTMLtoPDFService
    {
        private readonly IConverter _converter;

        public HTMLtoPDFService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] ConvertHTMLtoPDF(string htmlContent, string reportTitle, bool pageCount = true, bool portrait = true, bool headerLine = true)
        {
            string htmlHead = @$"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>{reportTitle}</title>
                            </head>
                            ";

            string htmlFooter = @$"<footer></footer>";

            var html = new StringBuilder();
            html.Append(htmlHead);
            html.Append(htmlContent);
            html.Append(htmlFooter);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation =portrait? Orientation.Portrait : Orientation.Landscape,
                PaperSize = PaperKind.A4},
                Objects = {new ObjectSettings() {
                                                  PagesCount = pageCount,
                                                  HtmlContent = html.ToString(),
                                                  WebSettings = { DefaultEncoding = "utf-8" },
                                                  HeaderSettings = { FontSize = 9,Center= reportTitle, Right = "Page [page] of [toPage]", Line = headerLine, Spacing = 2.812 },

            }}
            };
            byte[] pdf = _converter.Convert(doc);
            return pdf;
        }
    }


}
