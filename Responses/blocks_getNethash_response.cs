using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getNethash_response : BaseResponse
    {
        public string nethash;
    }
}
