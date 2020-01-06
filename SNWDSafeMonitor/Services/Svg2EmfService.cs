using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Svg;

namespace SNWDSafeMonitor.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Svg2EmfService
    {
        /// <summary>
        /// 转换svg到emf
        /// </summary>
        /// <param name="svgContent">svg</param>
        /// <returns>emf</returns>
        public static byte[] Convert(string svgContent)
        {
            string emfTempFile = Path.GetTempFileName();

            try
            {
                var svg = SvgDocument.FromSvg<SvgDocument>(svgContent);
                //var svg = SvgDocument.Open(svgPath);

                using (Graphics bufferGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
                {
                    using (var metafile = new Metafile(emfTempFile, bufferGraphics.GetHdc()))
                    {
                        using (Graphics graphics = Graphics.FromImage(metafile))
                        {
                            //Image image = Image.FromFile("C:/Users/Administrator/Desktop/1-1FF6102333526.png");
                            //Image image = Image.FromFile("C:/Users/Administrator/Desktop/ChMkJl4S2YSIGhPTABPmFchkIwsAAwKOQPn1-oAE-Yt013.jpg");
                            //graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                            //graphics.DrawImage(image, 0, 0, svg.Width, svg.Height);

                            //缩放并绘制背景图
                            //int svgWidth = (int) svg.Width;
                            //int svgHeight = (int) svg.Height;
                            //Image newimg = Resize(image, svgWidth, svgHeight, "W");
                            //graphics.DrawImage(newimg, 0, 0, svgWidth, svgHeight);

                            svg.Draw(graphics);
                        }
                    }
                }

                return File.ReadAllBytes(emfTempFile);
            }
            finally
            {
                File.Delete(emfTempFile);
            }
        }

        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="img">原始图片</param>  
        /// <param name="w">新的宽度</param>  
        /// <param name="h">新的高度</param>  
        /// <param name="mode">
        /// 1、"W"://指定宽，高按比例。
        /// 2、"H"://指定高，宽按比例。
        /// 3、"WH"://指定宽高缩放（可能变形）。
        /// 4、"Cut"://指定高宽裁减（不变形）。
        /// 5、"MaxWH"://最大宽高比例缩放，比如原100*50->50*30，则结果是50*25
        /// </param>  
        /// <returns>处理以后的图片</returns>  
        public static Image Resize(Image img, int w, int h, string mode)
        {
            try
            {
                int towidth = w;
                int toheight = h;

                int x = 0;
                int y = 0;
                int ow = img.Width;
                int oh = img.Height;

                switch (mode)
                {
                    case "W":   //指定宽，高按比例                    
                        toheight = img.Height * w / img.Width;
                        break;
                    case "H":   //指定高，宽按比例
                        towidth = img.Width * h / img.Height;
                        break;
                    case "WH":  //指定高宽缩放（可能变形）                
                        break;
                    case "Cut": //指定高宽裁减（不变形）                
                        if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
                        {
                            oh = img.Height;
                            ow = img.Height * towidth / toheight;
                            y = 0;
                            x = (img.Width - ow) / 2;
                        }
                        else
                        {
                            ow = img.Width;
                            oh = img.Width * h / towidth;
                            x = 0;
                            y = (img.Height - oh) / 2;
                        }
                        break;
                    case "MaxWH"://最大宽高比例缩放，比如原100*50->50*30，则结果是50*25
                        var rmaxhw_d1w = img.Width * 1.0 / w;
                        var rmaxhw_d2h = img.Height * 1.0 / h;
                        if (rmaxhw_d1w > rmaxhw_d2h)
                        {
                            if (rmaxhw_d1w <= 1)
                            {
                                towidth = img.Width; h = img.Height;
                                goto case "WH";
                            }
                            towidth = w;
                            goto case "W";
                        }
                        if (rmaxhw_d2h <= 1)
                        {
                            towidth = img.Width; h = img.Height;
                            goto case "WH";
                        }
                        toheight = h;
                        goto case "H";
                    default:
                        break;
                }

                Image b = new Bitmap(w, h);
                Graphics g = Graphics.FromImage(b);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic; // 插值算法的质量
                g.DrawImage(img, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();

                return b;
            }
            catch (Exception ex)
            {
                System.Console.Out.Write(ex.Message);
                return null;
            }
        }

    }

}
