using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Transaction_Object
    {
        public string amount;
        public string companyGeneratorPublicKey;
        public string confirmations;
        public string fee;
        public string id;
        public string recipientId;
        public string senderId;
        public string senderPublicKey;
        public string signature;
        public string signSignature;
        public string subtype;
        public string timestamp;
        public string type;
    }
}