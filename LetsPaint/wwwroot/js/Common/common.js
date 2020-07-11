$(document).ready(function () {
    $(document).on('click', '.lp-viewpassword', function () {
        var $txtPass = $(this).parent().parent().find('input');
        if ($(this).find('i').hasClass('fa-eye')) {
            $($txtPass).attr('type', 'text');
        }
        else {
            $($txtPass).attr('type', 'password');
        }
        $(this).find('i').toggleClass('fa-eye-slash').toggleClass('fa-eye');

    });
});

let _db = {
    msg: {
        setMsg: function (key, obj) {
            var oldData = JSON.parse(localStorage.getItem("notification"));
            if (oldData !== undefined && oldData !== null) {
                oldData.push(obj);
            }
            else {
                oldData = [obj];
            }
            localStorage.setItem("notification", JSON.stringify(oldData));
        },
        getMsg: function (index) {
            var oldData = localStorage.getItem("notification");
            if (oldData !== undefined && oldData !== null) {
                var obj = JSON.parse(oldData);
                if (index !== undefined)
                    return obj !== undefined ? obj[index] : {};
            }
            else {
                return obj !== undefined ? obj : [];
            }
        }
    }
}