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

    public class Service4 : Service3, IService4
    {
        public Byte[] GenerateLargeFakePdfFile(Double fileSizeInMegabytes)
        {
            if (fileSizeInMegabytes < 0)
            {
                throw new ArgumentOutOfRangeException("fileSizeInMegabytes", "File size must ba  poitive number");
            }
            Int32 filesizeInBytes = (Int32)(fileSizeInMegabytes * 1024 * 1024);
            if (filesizeInBytes > Int32.MaxValue)
            {
                throw new ArgumentOutOfRangeException("fileSizeInMegabytes", String.Format("File size cannot exceed {0:N2}mb", Int32.MaxValue / 1024 / 1024));
            }
            
            Byte[] fakePdfFile = new Byte[filesizeInBytes];
            new Random().NextBytes(fakePdfFile); //populate the file with random data
            return fakePdfFile;
        }
    }
}
