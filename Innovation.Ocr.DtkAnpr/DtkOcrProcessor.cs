using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using DTKANPRLib;

namespace IaG.State.Innovation.Ocr.DtkAnpr
{
    public class DtkOcrProcessor : IOcrProcessor, IDisposable
    {
        [DllImport("DTKANPR.dll", CharSet = CharSet.Unicode), PreserveSig]
        private static extern int CreateANPREngine(ref ANPREngine engine);
        [DllImport("DTKANPR.dll", CharSet = CharSet.Unicode), PreserveSig]
        private static extern int DestroyANPREngine(ANPREngine engine);

        //[DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);

        private ANPREngine engine = null;

        public DtkOcrProcessor()
        {
            Init();
        }

        private void Init()
        {
            // Create ANPR Engine object
            if (CreateANPREngine(ref engine) != 0)
            {
                //MessageBox.Show("Unable to create ANPR engine");
                return;
            }
            // Initialize engine
            try
            {
                engine.Init(false); // still image mode
            }
            catch (COMException ex)
            {
                //MessageBox.Show(ex.Message + " (ErrorCode = " + ex.ErrorCode.ToString() + ")");
                throw ex;
            }
            // Key
            engine.LicenseManager.AddLicenseKey("S108Y96AMZ13BBLD6BF8Q3T20");
        }



        #region IOcrProcessor Members

        public string ReadText(System.Drawing.Bitmap image)
        {
            string result = null;
            // This line fails in Azure deployment
            //IntPtr hBitmap = image.GetHbitmap();

            //engine.ReadFromBitmap((int)hBitmap, 0);
            
            // Reading from memory stream as opposed to gdi bitmap as deployment of gdi to Azure is problematic
            var memStream = new MemoryStream();
            image.Save(memStream, GetImageFormat(image.RawFormat.Guid));
            memStream.Position = 0;
            var buffer = memStream.ToArray();
            engine.ReadFromMemFile(buffer, buffer.Length, 0);
            //DeleteObject(hBitmap);
            //HttpContext.Current.Response.Write("Jelly Test 2 ### " + result);

            for (int i = 0; i < engine.Plates.Count; i++)
            {
                Plate plate = engine.Plates.get_Item(i);
                result = plate.Text;

                Marshal.ReleaseComObject(plate);
                plate = null;
            }
            if (engine.Plates.Count == 0) result = "*no match*";
            memStream.Dispose();
            GC.Collect();
            return result;
        }

        private ImageFormat GetImageFormat(Guid rawFormat)
        {
            if (rawFormat == ImageFormat.Gif.Guid)
                return ImageFormat.Gif;
            if (rawFormat == ImageFormat.Bmp.Guid)
                return ImageFormat.Bmp;
            if (rawFormat == ImageFormat.Jpeg.Guid)
                return ImageFormat.Jpeg;
            if (rawFormat == ImageFormat.Png.Guid)
                return ImageFormat.Png;
            if (rawFormat == ImageFormat.Tiff.Guid)
                return ImageFormat.Tiff;
            throw new ArgumentException("Not a valid image gif, png, bmp or tiff");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            DestroyANPREngine(engine);
        }

        #endregion
    }
}
