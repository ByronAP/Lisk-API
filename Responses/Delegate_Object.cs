using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Delegate_Object
    {
        public string address;
        public decimal approval;
        public long missedBlocks;
        public long producedBlocks;
        public decimal productivity;
        public string publicKey;
        public int rate;
        public string username;
        public string vote;
    }
}