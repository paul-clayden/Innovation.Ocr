using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Innovation.OcrQuoteBuy.Controllers;
using Innovation.OcrQuoteBuy.Tests.Properties;
using System.Drawing;
using System.Net;

namespace Innovation.OcrQuoteBuy.Tests.Controllers
{
    [TestClass]
    public class RegControllerTest
    {
        [TestMethod]
        public async void Post_valid_image_returns_registration_text()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            Resources.plate4.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            var httpClient = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>() { {"img", Encoding.ASCII.GetString(stream.ToArray()) } });
            HttpResponseMessage response = await httpClient.PostAsync("http://localhost:61445/api/Reg/", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

        }
    }
}
