var http = {};
http.send = function (url, options) {

    var deferred = $.Deferred();
    var p;

    if (options) {
        if (options.params) {
            url += "?" + $.param(options.params);
        }
        if (options.method && options.method == "GET") {
            p = $.get(url);
        }
        else if (options.method && options.method == "POST") {
            p = $.post(url, options.data);
        }
        else {
            p = $.get(url);
        }
    }
    else {
        p = $.get(url);
    }

    p.done(function (data) {
        var result = JSON.parse(data);
        if (result.errcode) {
            deferred.reject(result);
        }
        else {
            deferred.resolve(result);
        }
    }).fail(function (err) {
        try {
            var result = JSON.parse(err.responseText);
            if (result.errcode) {
                if (result.errcode == "110003") {
                    alert("出错了1");
                }
                else if (result.errcode == "101012") {

                } else {
                    deferred.reject(result);
                }
            }
            else {
                alert("出错了2");
            }
        }
        catch (err) {
            alert("出错了3");
        }

    });
    return deferred.promise();
}
function num(obj) {
    obj.value = obj.value.replace(/[^\d.]/g, ""); //清除"数字"和"."以外的字符
    obj.value = obj.value.replace(/^\./g, ""); //验证第一个字符是数字
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个, 清除多余的
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'); //只能输入两个小数
}
//<input type='text' onkeyup="this.value=this.value.replace(/[^0-9-]+/,'');" /> 只能输入0-9的整数