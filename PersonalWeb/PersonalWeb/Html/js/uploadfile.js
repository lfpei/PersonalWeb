$(function () {
    var filepath = "";
    $(".file").on("change", "input[type='file']", function () {
        var form = new FormData();
        form.append('file', $("#relfile")[0].files[0]);
        $.ajax({
            url: '/api/common/UpFile',
            data: form,
            method: 'post',
            contentType: false,
            processData: false,//此处是data的预处理，需要设置为false才可以
            success: function (data) {
                var obj = JSON.parse(data);
                if (obj.returnCode == "0000") {
                    filepath = obj.returnMsg;
                    var filename = $("#relfile")[0].files[0].name;
                    $("#filename").html(filename);
                }
                else {
                    layer.msg(obj.returnMsg);
                }
            }
        });
    })
    $("#QrCode").click(function () {
        var content = $("#content").val();
        var options = {
            "method": "GET",
            "params": { "url": content, "logoUrl": filepath },
        }
        var url = "/api/common/GetQRCode";
        http.send(url, options).then(function (data) {
            if (data.returnCode == "0000") {
                $("#qrCodeUrl").attr("src", data.returnMsg)
            }
            else {
                layer.msg(data.returnMsg);
            }
        })
    })
})

