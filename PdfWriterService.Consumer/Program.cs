using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using PdfWriterService.Consumer.PdfSvc;

namespace PdfWriterService.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte[] pdfFile;
            PdfWriterServiceV3Client client = new PdfWriterServiceV3Client();
            try
            {
                pdfFile = client.GenerateReburnPDF(new ReburnDTO
                    {
                        CustomrName = "Rapid Sheet Metal",
                        EmployeeResponsibleID = "MMOORE",
                        Comments = "Works on my machine (tm)",
                        Issue = "Unable to reproduce"
                    });

                using (FileStream fs = File.OpenWrite("Reburn.pdf"))
                {
                    fs.Write(pdfFile, 0, pdfFile.Length);
                    fs.Flush();
                }

                Console.WriteLine("PDF File Saved.");

                Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Path.Combine(Assembly.GetExecutingAssembly().Location, "Reburn.pdf")
                    }
                };
                proc.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex: {0}", ex.Message);
                client.Abort();
            }
            finally
            {
                if (client.State == System.ServiceModel.CommunicationState.Faulted)
                    client.Abort();
                else
                    client.Close();
            }
        }
    }
}
