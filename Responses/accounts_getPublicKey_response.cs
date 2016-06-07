using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_getPublicKey_response : BaseResponse
    {
        /// <summary>
        ///     Hex encoded public key
        /// </summary>
        public string publicKey;
    }
}