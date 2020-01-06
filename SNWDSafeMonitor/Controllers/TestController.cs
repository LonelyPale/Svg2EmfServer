using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SNWDSafeMonitor.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(Html))]
        public async Task<IActionResult> Html()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://www.baidu.com");

            if (response != null && response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return Ok("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(Image))]
        public async Task<IActionResult> Image()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://oscimg.oschina.net/oscnet/up-87b5b176adf6ec73dced53190667de201dd.png");

                if (response != null && response.IsSuccessStatusCode)
                {
                    byte[] result = await response.Content.ReadAsByteArrayAsync();
                    await Response.Body.WriteAsync(result, 0, result.Length);
                    return new EmptyResult();
                }
                else
                {
                    return Ok("");
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsync(ex.Message);
                return new EmptyResult();
            }
        }

    }

}
