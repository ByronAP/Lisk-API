using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_open_response : BaseResponse
    {
        public Account_Object account;
    }
}