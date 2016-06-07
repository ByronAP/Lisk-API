using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Peer_Object
    {
        public string ip;
        public string os;
        public int port;
        public int state;
        public string version;
    }
}