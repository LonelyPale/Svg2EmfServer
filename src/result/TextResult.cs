using System;
using BeetleX.Buffers;
using BeetleX.FastHttpApi;

namespace Svg2EmfServer
{
    public class TextResult : ResultBase
    {
        public TextResult(string text)
        {
            Text = text == null ? "" : text;
        }

        public string Text { get; set; }

        public override IHeaderItem ContentType => ContentTypes.TEXT_UTF8;

        public override bool HasBody => true;

        public override void Write(PipeStream stream, HttpResponse response)
        {
            stream.Write(Text);
        }
    }
}
