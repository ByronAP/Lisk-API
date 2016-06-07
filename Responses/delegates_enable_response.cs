using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_enable_response : BaseResponse
    {
        public Transaction_Object transaction;
    }
}