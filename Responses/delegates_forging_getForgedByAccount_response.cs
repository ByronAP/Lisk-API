using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_forging_getForgedByAccount_response : BaseResponse
    {
        public long sum;
    }
}