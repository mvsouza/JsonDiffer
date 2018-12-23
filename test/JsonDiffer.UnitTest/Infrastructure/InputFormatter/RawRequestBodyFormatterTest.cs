using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JsonDiffer.Infrastructure.InputFormattter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Xunit;

namespace JsonDiffer.UnitTest.Infrastructure.InputFormatter
{
    public class RawRequestBodyFormatterTest
    {
        [Fact]
        public async Task Should_convert_base64_encoded_json_on_a_StreamreaderAsync()
        {
            var content = "{\"key\":\"value\"}";
            string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(content));
            var memory = new MemoryStream((Encoding.ASCII.GetBytes(base64)));
            var reader = new StreamReader(memory);
            var result = await RawRequestBodyFormatter.MIMETypeToString(reader);

            Assert.Equal(content, result);
        }
    }
}
