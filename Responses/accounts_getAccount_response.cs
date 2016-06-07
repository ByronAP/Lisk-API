using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_getAccount_response : BaseResponse
    {
        public Account_Object account;
    }
}