$(function () {
    $("#bt").click(function () {
        var datas = $("#text1").val();
        $.get("api/values/Get?", { id: datas }, function (data) {
            $("#imgt").attr("src", data)
        })
    })
    $("#getPosition").click(function(){
        getLocation();
    })
})

function loadshutdown() {
    $.post("api/values/Post");
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        alert("浏览器不支持地理定位。");
    }
}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            alert("定位失败,用户拒绝请求地理定位");
            break;
        case error.POSITION_UNAVAILABLE:
            alert("定位失败,位置信息是不可用");
            break;
        case error.TIMEOUT:
            alert("定位失败,请求获取用户位置超时");
            break;
        case error.UNKNOWN_ERROR:
            alert("定位失败,定位系统失效");
            break;
    }
}
function showPosition(position) {
    var latlon = position.coords.latitude + ',' + position.coords.longitude;

    //google 
    var url = 'http://maps.google.cn/maps/api/geocode/json?latlng=' + latlon + '&language=CN';
    $.ajax({
        type: "GET",
        url: url,
        beforeSend: function () {
            $("#baidu_geo").html('正在定位...');
        },
        success: function (json) {
            if (json.status == 'OK') {
                var results = json.results;
                $.each(results, function (index, array) {
                    if (index == 0) {
                        $("#baidu_geo").html(array['formatted_address']);
                    }
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#baidu_geo").html(latlon + "地址位置获取失败");
        }
    });
}
//function showPosition(position) {
//    var lat = position.coords.latitude; //纬度 
//    var lag = position.coords.longitude; //经度 
//    alert('纬度:' + lat + ',经度:' + lag);
//}

//31.290016,121.50348

function getPosition1() {
    $.get("api/values/GetPosition",
        function (data) {
            alert(data);
        });
}

function Hidden() {
    //alert("1");
    var input = document.getElementsByTagName("input");
    for (var i = 0; i < input.length; i++) {
        input[i].style.display = "none";
    }
    $("#bthidden").click(function () {
        $("input").show();
    });
}