using System;
using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Data;

namespace Svg2EmfServer
{
    [Controller]
    public class EmfController
    {
        public EmfController()
        {
        }

        [Post(Route = "/")]
        [NoDataConvert]
        public object Convert(IHttpContext context)
        {
            //Console.WriteLine(context.Data);
            string value = context.Request.Stream.ReadString(context.Request.Length);
            var data = Svg2Emf.Convert(value);
            return new StreamResult(data);
        }
    }
}
