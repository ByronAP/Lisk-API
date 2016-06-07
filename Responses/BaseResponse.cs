using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class BaseResponse
    {
        public string error;
        public bool success;
    }
}