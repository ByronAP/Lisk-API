using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Account_Object
    {
        public string address;
        public string balance;
        public string publicKey;
        public string secondPublicKey;
        public string secondSignature;
        public string unconfirmedBalance;
        public string unconfirmedSignature;
        public string username;
    }
}