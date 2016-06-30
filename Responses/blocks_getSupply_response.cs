using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getSupply_response : BaseResponse
    {
        public long supply;
    }
}
