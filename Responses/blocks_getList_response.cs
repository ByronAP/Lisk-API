using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getList_response : BaseResponse
    {
        public List<Block_Object> blocks;
    }
}