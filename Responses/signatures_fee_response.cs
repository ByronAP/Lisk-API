using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class signatures_fee_response : BaseResponse
    {
        public long fee;
    }
}
