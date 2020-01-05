using System;
using System.IO;
using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Data;

namespace Svg2EmfServer
{
    [Controller(BaseUrl = "/test", SingleInstance = false)]
    public class TestController
    {
        public TestController()
        {
        }

        [Get]
        public object Text(IHttpContext context)
        {
            return new TextResult($"testing: {DateTime.Now}");
        }

        [Post]
        [NoDataConvert]
        public object File(IHttpContext context)
        {
            var stream = context.Request.Stream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return new StreamResult(bytes);
        }

        [Post]
        [NoDataConvert]
        public object PostStream(IHttpContext context)
        {
            //Console.WriteLine(context.Data);
            //string value = context.Request.Stream.ReadString(context.Request.Length);
            //return new TextResult(value);

            var stream = context.Request.Stream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return new StreamResult(bytes);
        }
    }
}
