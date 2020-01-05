using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Svg;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SNWDSafeMonitor.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : BaseController
    {

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id.Equals(0))
                return "00000000000000测试成功";
            else
                return "11111111111111测试成功";
        }
        [HttpGet]
        public IActionResult OK()
        {
            return Ok("返回了ok！！！！！");

        }

        //[HttpPost]
        //public byte[] Svg2Emf([FromBody] Byte[] svgdata)
        //{

        //    //var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));

        //    //var svg = SvgDocument.FromSvg<SvgDocument>(svgImage);
        //    Stream stream = new MemoryStream(svgdata);

        //    Stream emfstream = new MemoryStream();
        //    var svg = SvgDocument.Open<SvgDocument>(stream);
        //    using (Graphics bufferGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
        //    {
        //        using (var metafile = new Metafile(emfstream, bufferGraphics.GetHdc()))
        //        {
        //            using (Graphics graphics = Graphics.FromImage(metafile))
        //            {
        //                svg.Draw(graphics);
        //            }
        //        }
        //    }
        //    byte[] bytes = new byte[emfstream.Length];
        //    emfstream.Read(bytes, 0, bytes.Length);

        //    // 设置当前流的位置为流的开始 
        //    emfstream.Seek(0, SeekOrigin.Begin);
        //    return bytes;


        //}

        //public HttpResponseMessage Svg2Emf1([FromBody] Byte[] svgdata)
        //{

        //    //var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));
        //    var resp = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(svgdata)
        //        //或者
        //        //Content = new StreamContent(stream)
        //    };
        //    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
        //    return resp;
        //    //System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    //System.IO.Stream str = new FileUpload().PostedFile.InputStream;
        //    //System.Drawing.Bitmap map = new System.Drawing.Bitmap(str);
        //    //map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    //Response.ClearContent();
        //    //Response.ContentType = "image/gif";
        //    //Response.BinaryWrite(ms.ToArray());


        //}
    }
}
