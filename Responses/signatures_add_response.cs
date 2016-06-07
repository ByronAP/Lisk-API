using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class signatures_add_response : BaseResponse
    {
        public string publicKey;
        public string transactionId;
    }
}