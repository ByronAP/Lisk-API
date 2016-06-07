using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class peers_getVersion_response : BaseResponse
    {
        public string build;
        public string version;
    }
}