using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;

namespace SNWDSafeMonitor
{
    public class SVG2Emf
    {
   
        public static byte[] ConvertToEmf(string svgImage)
        {
            string emfTempPath = Path.GetTempFileName();
            try
            {
                //var svg = SvgDocument.FromSvg<SvgDocument>(svgImage);
                var svg = SvgDocument.Open<SvgDocument>(svgImage);
                using (Graphics bufferGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
                {
                    using (var metafile = new Metafile(emfTempPath, bufferGraphics.GetHdc()))
                    {
                        using (Graphics graphics = Graphics.FromImage(metafile))
                        {
                            svg.Draw( graphics);
                        }
                    }
                }

                return File.ReadAllBytes(emfTempPath);
            }
            finally
            {
                File.Delete(emfTempPath);
            }
        }
    }
}
