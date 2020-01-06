using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNWDSafeMonitor.Services;

namespace SNWDSafeMonitor.Controllers
{
    /// <summary>
    /// 把svg转换成emf
    /// </summary>
    [Route("api/svg2emf")]
    [ApiController]
    public class Svg2EmfController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 注入HttpClientFactory
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public Svg2EmfController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [Route("test")]
        [HttpGet]
        [HttpPost]
        public string Test()
        {
            QueryString query = Request.QueryString;
            string w = Request.Query["w"];
            string h = Request.Query["h"];
            string r = Request.Query["r"];
            string img = Request.Query["img"];
            string result = "";
            result += "w=" + w + "\n";
            result += "h=" + h + "\n";
            result += "r=" + r + "\n";
            result += "img=" + img + "\n";
            return "ok\n" + result;
        }

        /// <summary>
        /// 转换
        /// </summary>
        [Route("")]
        [HttpPost]
        public async void Convert()
        {
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string content = reader.ReadToEnd();

                //string img = Request.Query["img"];

                byte[] bytes = Svg2EmfService.Convert(content);

                Response.ContentType = "application/octet-stream";
                Response.Headers.Add("Content-Disposition", "attachment;filename=temp.emf");
                
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);

                /*
                using (BinaryWriter writer = new BinaryWriter(Response.Body))
                {
                    writer.Write(bytes);
                }
                */

            }
            catch (Exception ex)
            {
                await Response.WriteAsync(ex.Message);
            }
        }

    }

}
