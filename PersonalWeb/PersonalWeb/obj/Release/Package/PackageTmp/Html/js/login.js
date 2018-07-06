function register() {
    window.location.href = "register.html";
}
var mobile;
$(document).ready(function () {
    $(".msg").click(function () {
        mobile = $("#mobile").val();
        if (mobile == '') {
            alert("请输入手机号");
            return;
        }
        var countdown = 60;
        timeout(countdown);
        var options = {
            "method": "POST",
            "params": { "mobile": mobile, "nationcode": "86" },
        }
        var url = "/api/values/GetMsg";
        http.send(url, options).then(function (data) {
            if (data.returnCode != "00000") {
                alert(data.returnMsg);
            }
        })
    });
    $("#registerid").click(function () {
        var mobiles = $("#mobile").val();
        var userpwd = $("#userpwd").val();
        var msg = $("#msgid").val();
        if (mobiles == '') {
            alert("请输入手机号");
            return;
        }
        if (userpwd == '') {
            alert("密码不能为空");
            return;
        }
        if (userpwd.length < 8) {
            alert("密码不能少于8位");
            return;
        }
        if (msg == '') {
            alert("请输入短信验证码");
            return;
        }
        if (mobile != mobiles) {
            alert("手机号与短信验证码手机号不一致");
            return;
        }
        window.location.href = "../MyCompany/index.html";
    });
    $("#login").click(function () {
        window.location.href = "MyCompany/index.html";
    })
})

function timeout(countdown) {
    var interval = setInterval(function () {
        if (countdown > 0) {
            $(".msg").text(countdown);
            countdown--;
        }
        else {
            $(".msg").text("重新发送");
            clearInterval(interval);
        }
    }, 1000)
}