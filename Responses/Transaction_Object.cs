using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Transaction_Object
    {
        public long amount;
        public long confirmations;
        public long fee;
        public string id;
        public string recipientId;
        public string senderId;
        public string senderPublicKey;
        public string signature;
        public object signatures;
        public string timestamp;
        public string type;
        public object asset;
        public string blockId;
        public long height;
    }
}