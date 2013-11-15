using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace IaG.State.Innovation.Ocr.DtkAnpr
{
    public interface IOcrProcessor
    {
        string ReadText(Bitmap image);
    }
}
