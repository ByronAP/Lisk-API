using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class multisignatures_pending_response : BaseResponse
    {
        public List<Transaction_Object> transactions;
    }
}