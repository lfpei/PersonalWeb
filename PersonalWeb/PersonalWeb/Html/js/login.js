function register() {
    window.location.href = "register.html";
}

$(document).ready(function () {
    $(".msg").click(function () {
         var mobile = $("#mobile").val();
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
                layer.msg(data.returnMsg);
            }
        })
    });
    $("#registerid").click(function () {
        var mobiles = $("#mobile").val();
        var userpwd = $("#userpwd").val();
        var msg = $("#msgid").val();
        if (mobiles == '') {
            layer.msg("请输入手机号");
            return;
        }
        if (userpwd == '') {
            layer.msg("密码不能为空");
            return;
        }
        if (userpwd.length < 8) {
            layer.msg("密码不能少于8位");
            return;
        }
        if (msg == '') {
            layer.msg("请输入短信验证码");
            return;
        }
        var options = {
            "method": "POST",
            "params": { "mobile": mobiles, "pwd": userpwd, "msg": msg },
        }
        var url = "/api/values/Register";
        http.send(url, options).then(function (data) {
            if (data.retrunCode == "0000") {
                window.location.href = "MyCompany/index.html";
            }
            else {
                layer.msg(data.returnMsg);
            }
        })
        //window.location.href = "../MyCompany/index.html";
    });
    $("#login").click(function () {
        var username = $("#username").val();
        var pwd = $("#pwd").val();
        if (username == "") {
            layer.msg("用户名不能为空");
            return;
        }
        if (pwd == "") {
            layer.msg("密码不能为空");
            return;
        }
        var options = {
            "method": "POST",
            "params": { "username": username, "password": pwd },
        }
        var url = "/api/values/Login";
        http.send(url, options).then(function (data) {
            if (data.returnCode == "0000") {
                window.location.href = "MyCompany/index.html";
            }
            else {
                layer.msg(data.returnMsg);
            }
        })
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