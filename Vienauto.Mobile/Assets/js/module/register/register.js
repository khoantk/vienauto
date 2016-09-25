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

     
    $("#input-hoten-toregister").blur(function () {
        var _this_val = $(this).val();
        if (_this_val === "") {
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
        if (_this_val === "") {
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
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (_this_val === "") {
            $(this).addClass("error");
            $("#error_email").text("Bạn chưa nhập địa chỉ email.");
        }
        else if(!testEmail.test(_this_val)) {
            $(this).addClass("error");
            $("#error_email").text("Địa chỉ email không hợp lệ.");
        }
        else {
            $(this).removeClass("error");
            $("#error_email").text("");
        }
    });
    $("#input-pass-toregister").blur(function () {
        var _this_val = $(this).val();
        if (_this_val === "") {
            $(this).addClass("error");
            $("#error_password").text("Chưa nhập mật khẩu.");
        }
        else {
            $(this).removeClass("error");
            $("#error_password").text("");
        }
    });
    $("#repass-toverify").blur(function () {
        var _this_val = $(this).val();
        var _pass = $("#input-pass-toregister").val();
        if (_pass !== "" && _this_val === "") {
            $(this).addClass("error");
            $("#error_repass").text("Bạn chưa xác nhận mật khẩu.");
        }
        else if (_this_val !== _pass) {
            $(this).addClass("error");
            $("#error_repass").text("Mật khẩu xác nhận không trùng khớp.");
        }
        else {
            $(this).removeClass("error");
            $("#error_repass").text("");
        }
    });
    $("#select-quest-toregister").blur(function () {
        var elem = $(this).val();
        if (elem === "0") {
            $(this).addClass('error');
            $('#error_quest').text("Bạn chưa chọn câu hỏi bí mật.");
        }
        else {
            $(this).removeClass('error');
            $('#error_quest').text("");
        }
    });
    $("#input-answer-toregister").blur(function () {
        var _this_val = $(this).val();
        if (_this_val === "") {
            $(this).addClass("error");
            $("#error_answer").text("Chưa nhập câu trả lời. Nó rất cần thiết khi khôi phục mật khẩu.");
        }
        else {
            $(this).removeClass("error");
            $("#error_answer").text("");
        }
    });
    $("#input-phone-toregister").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && e.which !== 32 && (e.which >= 100 && e.which <= 122))
            return false;
    });
    $("#input-mobile-toregister").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && e.which !== 32 && (e.which >= 100 && e.which <= 122))
            return false;
    });
    $.validator.addMethod("validDropDownList", function (value, element) {
        return (value !== "0");
    }, "");
    function memberRegister(api, data) {
        $.ajax({
            url: api,
            data: data,
            type: "POST"
        }).success(function (response) {

        }).error(function (err) {

        });
    };
    $('#bt_actionregister_click').click(function () {
        var lastName = $("#input-hoten-toregister").val();
        var firstName = $("#input-ten-toregister").val();
        var email = $("#input-email-toregister").val();
        var pass = $("#input-pass-toregister").val();
        var repass = $("#repass-toverify").val();
        var selectQuest = $("#select-quest-toregister").val();
        var answer = $("#input-answer-toregister").val();
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        
        if (lastName !== "" && firstName !== "" && email !== "" && pass !== "" && repass !== "" && (pass === repass) && selectQuest !== "0" && answer !== "" && testEmail.test(email)) {
            if ($("#agree-with-scttos").is(":checked")) {
                $("#error_agree").text("");
                $("#validate_result").text("");
                
            }
            else {
                $("#error_agree").text("Vui lòng đồng ý các điều khoản của chúng tôi.");
            }
        }
        else {
            $("#validate_result").text("Vui lòng kiểm tra và điền đầy đủ các thông tin bắt buộc.");
        }
    });
});
