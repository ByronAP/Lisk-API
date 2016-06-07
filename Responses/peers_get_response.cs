using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class peers_get_response : BaseResponse
    {
        public Peer_Object peer;
    }
}