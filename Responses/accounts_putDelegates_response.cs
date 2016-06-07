using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class accounts_putDelegates_response : BaseResponse
    {
        //TODO what type should this be?
        public object transaction;
    }
}