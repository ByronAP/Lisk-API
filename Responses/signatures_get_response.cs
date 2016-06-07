using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class signatures_get_response : BaseResponse
    {
        public Signature_Object signature;
    }
}