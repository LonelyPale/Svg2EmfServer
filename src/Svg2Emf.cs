using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Svg;

namespace Svg2EmfServer
{
    public class Svg2Emf
    {
        public Svg2Emf()
        {
        }

        public static byte[] Convert(string svgImage)
        {
            string emfTempPath = Path.GetTempFileName();

            try
            {
                var svg = SvgDocument.FromSvg<SvgDocument>(svgImage);
                //var svg = SvgDocument.Open(svgImage);

                using (Graphics bufferGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
                {
                    using (var metafile = new Metafile(emfTempPath, bufferGraphics.GetHdc()))
                    {
                        using (Graphics graphics = Graphics.FromImage(metafile))
                        {
                            svg.Draw(graphics);
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
