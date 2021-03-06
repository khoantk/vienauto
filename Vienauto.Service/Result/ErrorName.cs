﻿namespace Vienauto.Service.Result
{
    public enum ErrorCode
    {
        None = 0,
        Exception = 1,
        LogInFail,
        DuplicateUser,
        RegisterUserFail,
        RegisterAgentFail,
        RegisterAgentUserFail,
        FailToListAll,
        FailToListAllQuestion,
        FailToListAllDealerShip,
        FailToListLocation,
        FailToListGetAgencyByDealerShip
    }
}
