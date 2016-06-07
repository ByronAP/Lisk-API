using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_install_response : BaseResponse
    {
        public string path;
    }
}