using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_ismasterpasswordenabled_response : BaseResponse
    {
        public bool enabled;
    }
}
