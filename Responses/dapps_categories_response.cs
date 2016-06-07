using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_categories_response : BaseResponse
    {
        public List<KeyValuePair<string, int>> categories;
    }
}