using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_fee_response : BaseResponse
    {
        public long fee;
    }
}
