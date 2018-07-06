$(function () {
    /*动态显示参数为类名
      需要循环动态显示时，加上setInterval(dynamic,500);
    */
    function dynamic() {
        $('p').toggleClass(function () {
            if ($(this).hasClass('class1')) {
                $(this).removeClass('class1');
                return 'class2';
            } else if ($(this).hasClass('class2')) {
                $(this).removeClass('class2');
                return 'class3';
            } else {
                $(this).removeClass('class3');
                return 'class1'
            }
        })
    }
})