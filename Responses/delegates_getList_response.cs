using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_getList_response : BaseResponse
    {
        public List<Delegate_Object> delegates;
        public int totalCount;
    }
}