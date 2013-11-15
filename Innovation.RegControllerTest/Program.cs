using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IaG.State.Innovation.RegControllerTest.Properties;

namespace IaG.State.Innovation.RegControllerTest
{
    class Program
    {
        const string RegistrationControllerUri = "http://ocrrego.azurewebsites.net/";


        static void Main(string[] args)
        {
            SendWebRequest();
        }

        private static void SendWebRequest()
        {
            do
            {
                try
                {
                    
                    Console.WriteLine(string.Format("Request : {0}api/reg", RegistrationControllerUri));
                    var request = WebRequest.Create(RegistrationControllerUri);
                    request.Method = "Post";
                    request.Timeout = 20000;
                    var stream = request.GetRequestStream();
                    Resources.plate4.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Flush();
                    var response = request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    var length = response.ContentLength;
                    byte[] buffer = new byte[length];
                    responseStream.Read(buffer, 0, Convert.ToInt32(length));
                    Console.WriteLine();
                    Console.WriteLine("Response : " + Encoding.ASCII.GetString(buffer));
                    response.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.WriteLine();
                Console.WriteLine("Another (y/n) ?");
            }
            while (Console.ReadLine() == "y");
        }
    }
}
