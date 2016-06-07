using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class multisignatures_sign_response : BaseResponse
    {
        public string transactionId;
    }
}