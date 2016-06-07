﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lisk.API.Responses
{
    [JsonObject(MemberSerialization = MemberSerialization.Fields)]
    public class transactions_getList_response : BaseResponse
    {
        public List<Transaction_Object> transactions;
    }
}