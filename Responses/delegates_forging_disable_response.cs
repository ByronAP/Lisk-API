using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_forging_disable_response : BaseResponse
    {
        public string address;
    }
}