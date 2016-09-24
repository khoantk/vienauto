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

     
    $("#input-hoten-toregister").blur(function () {
        var _this_val = $(this).val();
        if (isEmpty(_this_val)) {
            $(this).addClass("error");
            $("#error_hoten").text("Chưa nhập họ và chữ lót của bạn.");
        }
        else {
            $(this).removeClass("error");
            $("#error_hoten").text("");
        }
    });
    $("#input-ten-toregister").blur(function () {
        var _this_val = $(this).val();
        if (isEmpty(_this_val)) {
            $(this).addClass("error");
            $("#error_hoten").text("Chưa nhập tên của bạn.");
        }
        else {
            $(this).removeClass("error");
            $("#error_hoten").text("");
        }
    });
    $("#input-email-toregister").blur(function () {
        var _this_val = $(this).val();
        if (isEmpty(_this_val) || _this_val.indexOf("@") == -1 || _this_val.indexOf(".") == -1) {
            $(this).addClass("error");
            $("#error_email").text("Địa chỉ email không tồn tại.");
        }
        else {
            $(this).removeClass("error");
            $("#error_email").text("");
        }
    });
    $("#input-pass-toregister").blur(function () {
        var _this_val = $(this).val();
        if (isEmpty(_this_val)) {
            $(this).addClass("error");
            $("#error_password").text("Chưa nhập mật khẩu.");
        }
        else {
            $(this).removeClass("error");
            $("#error_password").text("");
        }
    });
    $("#repasss-toverify").blur(function () {
        var _this_val = $(this).val();
        var _pass = $("#input-pass-toregister").val();
        if (_this_val != _pass) {
            $(this).addClass("error");
            $("#error_repass").text("Mật khẩu xác nhận không trùng khớp.");
        }
        else {
            $(this).removeClass("error");
            $("#error_repass").text("");
        }
    });
    $("#input-answer-toregister").blur(function () {
        var _this_val = $(this).val();
        if (isEmpty(_this_val)) {
            $(this).addClass("error");
            $("#error_answer").text("Chưa nhập câu trả lời. Nó rất cần thiết khi khôi phục mật khẩu.");
        }
        else {
            $(this).removeClass("error");
            $("#error_answer").text("");
        }
    });
});
