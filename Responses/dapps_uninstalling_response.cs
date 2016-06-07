using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_uninstalling_response : BaseResponse
    {
        public List<string> uninstalling;
    }
}