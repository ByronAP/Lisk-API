using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class dapps_get_response : BaseResponse
    {
        public DAPP_Object dapp;
    }
}