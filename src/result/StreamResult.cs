using System;
using BeetleX.Buffers;
using BeetleX.FastHttpApi;

namespace Svg2EmfServer
{
    public class StreamResult : IResult
    {
        public StreamResult(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }

        public IHeaderItem ContentType => ContentTypes.OCTET_STREAM;

        public int Length { get; set; }

        public bool HasBody => true;

        public void Setting(HttpResponse response)
        {
            //response.Header.Add("Content-Disposition", "attachment;filename=test.txt");
        }

        public void Write(PipeStream stream, HttpResponse response)
        {
            stream.Write(Data, 0, Data.Length);
        }
    }
}
