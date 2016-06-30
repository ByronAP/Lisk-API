using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getMilestone_response : BaseResponse
    {
        public long milestone;
    }
}
