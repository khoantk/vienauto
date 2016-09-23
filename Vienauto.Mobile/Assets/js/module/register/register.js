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

    $('#radAgent').click(function () {
        $('#liMember').show();
        $('#liAgent').show();

    });

    $('#radMember').click(function () {
        $('#liMember').show();
        $('#liAgent').hide();
        $('#agentForm').removeClass('active');
        $('#agentForm').hide();
        $('#memberForm').show();
        $('#liMember').addClass('active');
    });

    $('#liMember').click(function () {
        $('#memberForm').addClass('active');
        $('#memberForm').show();
        $('#agentForm').removeClass('active');
        $('#agentForm').hide();
        $('#liMember').addClass('active');
        $('#liAgent').removeClass('active');
    });

    $('#liAgent').click(function () {
        $('#memberForm').removeClass('active');
        $('#memberForm').hide();
        $('#agentForm').addClass('active');
        $('#agentForm').show();
        $('#liMember').removeClass('active');
        $('#liAgent').addClass('active');
    });
});
