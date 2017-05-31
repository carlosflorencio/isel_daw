using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSiren.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Net.Http.Headers;

namespace API.Formatters
{
    public class SirenOutputFormatter : TextOutputFormatter
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public SirenOutputFormatter() : this(DefaultJsonSerializerSettings, DefaultEncoding)
        {
        }

        public SirenOutputFormatter(JsonSerializerSettings settings) : this(settings, DefaultEncoding)
        {
        }

        public SirenOutputFormatter(Encoding encoding) : this(DefaultJsonSerializerSettings, encoding)
        {
        }

        public SirenOutputFormatter(JsonSerializerSettings settings, Encoding encoding)
        {
            _jsonSerializerSettings = settings;
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/vnd.siren+json"));
            SupportedEncodings.Add(encoding);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(SirenEntity);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            return context.HttpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(context.Object, _jsonSerializerSettings),
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }
    }

}
