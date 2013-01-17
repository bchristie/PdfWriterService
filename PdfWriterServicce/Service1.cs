using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfWriterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public String HelloWorld()
        {
            return "Hello Bob!";
        }
    }

    public class Service2 : Service1, IService2
    {
        public string HelloWorld(string name)
        {
            return String.Format("Hello, {0}!", name);
        }
    }

    public class Service3 : Service2, IService3
    {
        public Byte[] GenerateReburnPDF(Models.ReburnDTO reburnDto)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reburn Request";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            gfx.DrawString("Reburn Request", font, XBrushes.Black,
                new XRect(0,0, page.Width, page.Height),
                XStringFormats.TopCenter);
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            return ms.ToArray();
        }
    }
}
