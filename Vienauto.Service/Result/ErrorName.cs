using System;
using System.Collections.Generic;

namespace Vienauto.Service.Result
{
    public enum ErrorCode
    {
        None = 0,
        Exception = 1,
        LogInFail,
        FailToListAll,
        FailToListAllQuestion,
        FailToListAllDealerShip,
        FailToListLocation
    }
}
