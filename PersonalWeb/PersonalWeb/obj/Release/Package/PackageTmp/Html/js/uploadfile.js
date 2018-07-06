$(function () {
    $(".file").on("change", "input[type='file']", function () {
    })
})
$("#QQQQ").ajaxSubmit({
    type: 'post',
    url: '/Controllers/Handler1.ashx',
    success: function (data) {
        alert(data);
    },
    error: function (data) {
        alert(data);
    }
});
var uploadajax3 = function () {
    var form = new FormData();
    form.append('file', $("#File2")[0].files[0]);
    $.ajax({
        url:'/Controllers/Handler1.ashx',
        data:form,
        method: 'post',
        contentType: false,
        processData: false,//此处是data的预处理，需要设置为false才可以
    });
}

var uploadajax2 = function () {
     
    $("#uploadajax").ajaxSubmit({
        url: '/Controllers/Handler1.ashx',
        type: 'post'
    });


} 

