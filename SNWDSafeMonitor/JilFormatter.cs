
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNWDSafeMonitor
{
    // Formatter 基类
    public abstract class MyFormatter
    {
        protected const string CONTENT_TYPE = "application/json";

        //protected readonly Options Opts = new Options(excludeNulls: false, includeInherited: true,
        //    //dateFormat: DateTimeFormat.SecondsSinceUnixEpoch,
        //    dateFormat: DateTimeFormat.ISO8601,
        //                                              serializationNameFormat: SerializationNameFormat.CamelCase);
        protected readonly Options Opts = new Options(dateFormat: DateTimeFormat.ISO8601);
    }

    // 输入 JSON 解析类
    public class MyInputFormatter : MyFormatter, IInputFormatter
    {
        private readonly Options _options;

        public MyInputFormatter()
            : this(null)
        { }

        public MyInputFormatter(Options options)
        {
            _options = options ?? Opts;
        }

        public bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.HttpContext.Request;

            using (var reader = context.ReaderFactory(request.Body, Encoding.UTF8))
            {
                // 使用 Jil 反序列化
                var result =JSON.Deserialize(reader, context.ModelType, _options);
                return InputFormatterResult.SuccessAsync(result);
            }
        }
    }

    // 输出 JSON 解析类
    public class MyOutputFormatter : MyFormatter, IOutputFormatter
    {
        private readonly Options _options;

        public MyOutputFormatter()
            : this(null)
        { }

        public MyOutputFormatter(Options options)
        {
            _options = options ?? Opts;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = CONTENT_TYPE;

            if (context.Object == null)
            {
                // 忘了在哪里看的了，192 好像在 Response.Body 中表示 null
                response.Body.WriteByte(192);
                return Task.CompletedTask;
            }

            using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
            {
                // 使用 Jil 序列化
                JSON.Serialize(context.Object, writer, _options);
                return Task.CompletedTask;
            }
        }
    }
}