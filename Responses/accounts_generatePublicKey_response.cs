using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_generatePublicKey_response : BaseResponse
    {
        /// <summary>
        ///     The public key in hex
        /// </summary>
        public string publicKey;
    }
}