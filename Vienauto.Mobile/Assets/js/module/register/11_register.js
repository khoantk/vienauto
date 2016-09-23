$(document).ready(function () {
    $("#signUpLink").click(function () {
        window.location = "#dang-ky-thanh-vien";
    });

    Ajax.postOnChange($("#DealerShips"), function () { return $("#DealerShips").val(); });
    $("#DealerShips").on("ajaxSuccess", function (target, response) {
        var result = respond.data;
        if (typeof result != 'undefined' && typeof result != null) {
            var ddlInnerHtml = "";
            $.each(result, function (item) {
                ddlInnerHtml += "<option value='" + item.AgencyId + "'>" + item.FullName + "</option>";
            });
            $("#ddlAgents").html(ddlInnerHtml);
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
