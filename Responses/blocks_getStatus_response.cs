using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getStatus_response : BaseResponse
    {
        public long height;
        public long fee;
        public long milestone;
        public long reward;
        public long supply;
    }
}
