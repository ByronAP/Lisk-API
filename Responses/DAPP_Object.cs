using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class DAPP_Object
    {
        public int category;
        public string description;
        public string icon;
        public string link;
        public string name;
        public string tags;
        public string transactionId;
        public int type;
    }
}