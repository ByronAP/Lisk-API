using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_getVotes_response : BaseResponse
    {
        public List<Delegate_Object> delegates;
    }
}