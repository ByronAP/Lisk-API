using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class multisignatures_createAccount_response : BaseResponse
    {
        public string transactionId;
    }
}