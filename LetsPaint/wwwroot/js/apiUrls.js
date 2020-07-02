var baseUrl = "";
if (location.host.indexOf('localhost') > -1) {
    baseUrl = "http://localhost:59928";
} else {
    baseUrl = "http://letspaint.somee.com";
}
const url = {
    root: {
        home: {},
        auth: {
            changePassword: '/auth/changePassword',
            isEmailexist:'/auth/isEmailexist'
        },
        query: {
            sendQuery:'/query/sendQuery'
        },
        gallery: {
            getgalleryType: '/gallery/getgalleryType',
            getGallery: '/gallery/getgallery',
        }
    }
}

$(document).ajaxStart(function () {
    $('.lp-loader').show();
});

$(document).ajaxComplete(function () {
    $('.lp-loader').hide();
});