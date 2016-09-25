$(document).ready(function () {
    window.location.hash = "#dang-ky-thanh-vien";

    $("#liMember").addClass("focus");
    $("#btnUserRegister").hide();
    $("#IsRegsiterAgent").val("True");

    $("#radMember").click(function () {
        if ($("#liAgent").hasClass("focus"))
            $("#liAgent").removeClass("focus");

        if ($("#liAgent").hasClass("active"))
            $("#liAgent").removeClass("active");

        $("#liAgent").hide();

        $("#liMember").addClass("active");
        $("#liMember").addClass("focus");
        
        $("#memberForm").addClass("active");
        if ($("#agentForm").hasClass("active"))
            $("#agentForm").removeClass("active");
        
        $("#btnUserRegister").show();
        $("#IsRegsiterAgent").val("False");
    });

    $("#radAgent").click(function () {
        $("#liMember").addClass("focus");
        $("#liAgent").addClass("active");
        if ($("#liAgent").hasClass("focus"))
            $("#liAgent").removeClass("focus");

        $("#memberForm").addClass("active");
        if ($("#agentForm").hasClass("active"))
            $("#agentForm").removeClass("active");

        $("#btnUserRegister").hide();
        $("#IsRegsiterAgent").val("True");
    });

    $("ul#tabs li").click(function (e) {
        if (!$(this).hasClass("focus")) {
            var tabNum = $(this).index();
            var nthChild = tabNum + 1;
            $("ul#tabs li.focus").removeClass("focus");
            $(this).addClass("focus");
            if ($("ul#tab li.tab").hasClass("active")) {
                $("ul#tab li.tab").removeClass("active");
            }
            $("ul#tab li.tab:nth-child(" + nthChild + ")").addClass("active");
        }
    });

    Ajax.postOnChange($("#DealerShipId"), function () { return $("#DealerShips").val(); });
    $("#DealerShipId").on("ajaxSuccess", function (target, response) {
        var result = respond.data;
        if (typeof result != 'undefined' && typeof result != null) {
            var ddlInnerHtml = "";
            $.each(result, function (item) {
                ddlInnerHtml += "<option value='" + item.AgencyId + "'>" + item.FullName + "</option>";
            });
            $("#AgentId").html(ddlInnerHtml);
        }
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
	$("#btnAgentRegister").click(function () {
        $("frmRegister").validate({
            rules: {
                FirstName: "required",
                LastName: "required",
                Email: {
                    required: true,
                    email: true
                },
                Password: "required",
                ConfirmPassword: {
                    required: true,
                    equalTo: "#Password"
                },
                QuestionId: "required",
                Answer: "required",
                CompanyName: "required",
                TransactionAddress: "required"
            },
            messages: {
                FirstName: "Chưa nhập họ và chữ lót của bạn.",
                LastName: "Chưa nhập tên của bạn.",
                Email: {
                    required: "Chưa nhập email",
                    email: "Địa chỉ email không tồn tại."
                },
                Password: "Chưa nhập mật khẩu",
                ConfirmPassword: {
                    required: "Chưa nhập xác nhận mật khẩu",
                    equalTo: "Mật khẩu xác nhận không trùng khớp."
                },
                QuestionId: "Chưa chọn câu hỏi bí mật",
                Answer: "Chưa nhập câu trả lời. Nó rất cần thiết khi khôi phục mật khẩu.",
                CompanyName: "Chưa nhập tên công ty.",
                TransactionAddress: "Chưa nhập địa chỉ giao dịch."
            },
            submitHandler: function (form) {
                form.submit();
            }
        });
    });
});
