using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class transactions_getUnconfirmed_response : BaseResponse
    {
        public Transaction_Object transaction;
    }
}