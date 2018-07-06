document.write("<script language=javascript src='js/common.js'><\/script>");
$(document).ready(function () {
    $("#btselect").click(function () {
        var sql = $("#txtContent").val();
        var url = "/api/values/GetUserInfo";
        var options = {
            "method": "GET",
            "params": {
                "sql": sql
            }
        }
        http.send(url, options).then(function (data) {
            
            $("#txtContent").val(data[0]);
        })
    }),
    $("#btsave").click(function () {
        var sql = $("#txtContent").val();
        $.ajax({
            url: "/api/values/Post?sql=" + sql,
            type: "Post",
            dataType: "json",
            success: function (data) {
                var d = JSON.parse(data);
                $("#txtContent").val(d.returnMsg);
            }
        });
    })
})




