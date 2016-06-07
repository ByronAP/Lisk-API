using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Block_Object
    {
        public string blockSignature;
        public long confirmations;
        public string generatorId;
        public string generatorPublicKey;
        public long height;
        public string id;
        public int numberOfTransactions;
        public string payloadHash;
        public long payloadLength;
        public string previousBlock;
        public long reward;
        public long timestamp;
        public string totalAmount;
        public long totalFee;
        public string totalForged;
        public string version;
    }
}