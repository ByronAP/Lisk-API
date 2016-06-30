using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class Fees_Object
    {
        public long send;
        public long vote;
        public long secondsignature;
        public long @delegate;
        public long multisignature;
        public long dapp;
    }
}