using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class delegates_get_response : BaseResponse
    {
        public Delegate_Object @delegate;
    }
}