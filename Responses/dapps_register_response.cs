using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_register_response : BaseResponse
    {
        public string transactionId;
    }
}