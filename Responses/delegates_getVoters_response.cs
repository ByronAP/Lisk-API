using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_getVoters_response : BaseResponse
    {
        public List<Account_Object> accounts;
    }
}