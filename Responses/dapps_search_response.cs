using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_search_response : BaseResponse
    {
        public List<DAPP_Object> dapps;
    }
}