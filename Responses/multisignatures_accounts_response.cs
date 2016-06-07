using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class multisignatures_accounts_response : BaseResponse
    {
        public List<Account_Object> accounts;
    }
}