using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class transactions_send_response : BaseResponse
    {
        public string transactionId;
    }
}