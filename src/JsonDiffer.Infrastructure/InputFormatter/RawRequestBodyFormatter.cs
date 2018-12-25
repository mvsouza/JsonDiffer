using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

[assembly: InternalsVisibleTo("JsonDiffer.UnitTest.Infrastructure.InputFormattterTest")]

namespace JsonDiffer.Infrastructure.InputFormattter
{
    public class RawRequestBodyFormatter : InputFormatter
    {
        public RawRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
        }
        
        public override Boolean CanRead(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return SupportedMediaTypes.Any(m => context.IsFromContentType(m));
        }
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;

            if (context.IsFromContentType("text/json"))
            {

                using (var reader = new StreamReader(request.Body))
                {
                    var content = await MIMETypeToString(reader);

                    return await InputFormatterResult.SuccessAsync(content);
                }
            }
            return await InputFormatterResult.FailureAsync();
        }

        public static async Task<string> MIMETypeToString(StreamReader reader)
        {
            var encodedString = await reader.ReadToEndAsync();
            byte[] data = Convert.FromBase64String(encodedString);
            string content = Encoding.UTF8.GetString(data);
            return content;
        }
    }
    public static class InputFormatterContextExtension {
        
        public static bool IsFromContentType(this InputFormatterContext context, string contentType)
        {
            var formatterContentType = context.HttpContext.Request.ContentType;
            return string.IsNullOrEmpty(formatterContentType) || formatterContentType.Contains(contentType);
        }
}
}
