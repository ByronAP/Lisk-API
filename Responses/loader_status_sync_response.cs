using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class loader_status_sync_response : BaseResponse
    {
        public long blocks;
        public long height;
        public bool syncing;
    }
}