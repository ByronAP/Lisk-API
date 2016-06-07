using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class loader_status_response : BaseResponse
    {
        public long blocksCount;
        public bool loaded;
    }
}