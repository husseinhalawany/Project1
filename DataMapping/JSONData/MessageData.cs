using DataMapping.Interfaces;
using Newtonsoft.Json;

namespace DataMapping.JSONData
{
    public class MessageData : IJson
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public IJson InitByJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<MessageData>(jsonObject);
        }
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}