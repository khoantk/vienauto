namespace Vienauto.Service.Result
{
    public enum ErrorCode
    {
        None = 0,
        Exception = 1,
        LogInFail,
        RegisterFail,
        FailToListAll,
        FailToListAllQuestion,
        FailToListAllDealerShip,
        FailToListLocation,
        FailToListGetAgencyByDealerShip
    }
}
