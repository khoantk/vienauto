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

    $("#input-phone-toregister").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && e.which !== 32 && (e.which >= 100 && e.which <= 122))
            return false;
        $('#input-phone-toregisteragent').val($(this).val());
    });
    $("#input-mobile-toregister").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && e.which !== 32 && (e.which >= 100 && e.which <= 122))
            return false;
        $('#input-mobile-toregisteragent').val($(this).val());
    });
    $("#input-email-toregister").blur(function () {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(email))
            $("#input-email-toregisteragent").val($(this).val());
    });
    $('#btnUserRegister').click(function () {
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
                Answer: "required"                
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
                Answer: "Chưa nhập câu trả lời. Nó rất cần thiết khi khôi phục mật khẩu."                
            },
            submitHandler: function (form) {
                form.submit();
            }
        });
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
