using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class peers_getList_response : BaseResponse
    {
        public List<Peer_Object> peers;
    }
}