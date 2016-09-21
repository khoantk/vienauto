$(document).ready(function () {
    $("#signUpLink").click(function () {
        window.location = "#dang-ky-thanh-vien";
    });

    //data-href="/account/GetAgencyDealerShip/@item.Id"

    Ajax.post($("#dealerShipDDL"), function () { });
    $("#dealerShipDDL").on("ajaxSuccess", function (target, response) {
        if (typeof response.data != 'undefined' && typeof response.data != null) {

        }
    });
});
