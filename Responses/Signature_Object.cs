using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Signature_Object
    {
        public string generationSignature;
        public string generatorPublicKey;
        public string id;
        public string publicKey;
        public List<object> signature;
        public long timestamp;
    }
}