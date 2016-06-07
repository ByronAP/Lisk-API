using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getHeight_response : BaseResponse
    {
        public long height;
    }
}