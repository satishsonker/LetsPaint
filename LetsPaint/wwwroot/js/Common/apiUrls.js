

const apiURLs = {
    baseUrl:'',
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
    },
    admin: {
        userManagement: {
            getUserList: '/admin/usermanagement/getuserlist',
            blockUser: '/admin/usermanagement/BlockUser',
            deleteUser: '/admin/usermanagement/DeleteUser'
        }
    }
}
if (location.host.indexOf('localhost') > -1) {
    apiURLs.baseUrl = "http://localhost:59928";
} else {
    apiURLs.baseUrl = "http://letspaint.somee.com";
}

$(document).ajaxStart(function () {
    $('.lp-loader').show();
});

$(document).ajaxComplete(function () {
    $('.lp-loader').hide();
});