using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using PdfWriterService.Consumer.PdfServiceV4;

namespace PdfWriterService.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfWriterServiceV4Client client = new PdfWriterServiceV4Client();
            try
            {
                Double[] testFileSizes = new[] { 0.5, 1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0 };
                foreach (var testFileSize in testFileSizes)
                {
                    Console.Write("Attempting to receive a PDF {0:n2}mb in size...", testFileSize);
                    try 
	                {	        
		                Byte[] pdfFile = client.GenerateLargeFakePdfFile(testFileSize);
                        Console.WriteLine("SUCCESS");
	                }
	                catch (Exception)
	                {
		                Console.WriteLine("FAILURE (Check maxReceivedMessageSize)");
	                }
                }
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
