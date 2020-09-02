

const apiURLs = {
    baseUrl:'',
    root: {
        home: {},
        base: {
            getReflookupData: '/base/GetRefLookupData'
        },
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
        },
        galleryManagement: {
            getGalleryType: '/admin/galleryManagement/getGalleryType',
            updateGalleryType: '/admin/galleryManagement/updateGalleryType',
            saveGalleryType: '/admin/galleryManagement/saveGalleryType',
            deleteGalleryType: '/admin/galleryManagement/deleteGalleryType',
            getGallery: '/admin/galleryManagement/getGallery',
            addGallery: '/admin/galleryManagement/addGallery',
            updateGallery: '/admin/galleryManagement/updateGallery',
            saveGallery: '/admin/galleryManagement/saveGallery',
            deleteGallery: '/admin/galleryManagement/deleteGallery',
            deleteGalleryImage: '/admin/galleryManagement/deleteGalleryImage',
            getDropdownData: '/admin/galleryManagement/getDropdownData',
        }
    }
}

const pageUrls = {
    admin: {
        userManagement: {
        },
        galleryManagement: {
            galleryList: '/admin/galleryManagement/gallery',
            addGalleryType: '/admin/galleryManagement/addGalleryType',
            galleryTypeList: '/admin/galleryManagement/GalleryType',
            addGallery: '/admin/galleryManagement/addGallery',
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