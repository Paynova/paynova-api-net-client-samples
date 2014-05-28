using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MvcSample.Core.Serialization
{
    public class JsonSerializer : ISerializer
    {
        protected JsonSerializerSettings Settings { get; private set; }

        public JsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new[] { new StringEnumConverter() }
            };
        }

        public virtual string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item, Settings);
        }
    }
}