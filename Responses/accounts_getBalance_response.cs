using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_getBalance_response : BaseResponse
    {
        public string balance;
        public string unconfirmedBalance;
    }
}