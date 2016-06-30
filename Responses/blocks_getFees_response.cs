using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class blocks_getFees_response : BaseResponse
    {
        public Fees_Object fees;
    }
}
