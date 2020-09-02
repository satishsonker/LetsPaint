$(document).ready(function () {
    let $dxDataGrid;
    var $param = {
        pageNo: 1,
        pageSize: 30
    };
    api.http.post(apiURLs.admin.galleryManagement.getGallery, $param).then(function (data) {
        $dxDataGrid = $("#gridContainer").dxDataGrid({
            dataSource: data.data,
            showBorders: true,
            showRowLines: true,
            scrolling: {
                mode: "standard" // or "virtual" | "infinite"
            },
            filterRow: { visible: false },
            headerFilter: { visible: false },
            paging: {
                pageSize: 10
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20],
                showInfo: true
            },
            columns: [{
                caption: "Action",
                width: 125,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div class="action-panel">`)
                        .append(`<ul>
                                    <li title="Edit"><a href="/admin/GalleryManagement/addGallery?galleryid=${options.data.galleryId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteGalleryType(${options.data.galleryTypeId})"><i class="la la-trash"></i></li>
                                    </ul>`)
                        .appendTo(container);
                }
            }, {
                caption: "Sr. No.",
                width: 70,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div>`)
                        .append(`<span>${(options.rowIndex + 1)}</span>`)
                        .appendTo(container);
                }
                }, {
                    caption: "Image",
                    width: 70,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $(`<div>`)
                            .append(`<img data-toggle="modal" data-target="#galleryImage" data-imagepath="${options.data.baseUrl + options.data.image}" src="${options.data.baseUrl+options.data.thumbnail}" style="width:50px;height:50px;border-radius:36px;cursor:pointer;" />`)
                            .appendTo(container);
                    }
                }, "galleryType", "gridSize", "baseUrl"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "fab fa-canadian-maple-leaf",
                        hint: 'Add Gallery',
                        onClick: function (e) {
                            app.methods.url.redirectTo(pageUrls.admin.galleryManagement.addGallery);
                        }
                    }
                },{
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "fab fa-pagelines",
                        hint: 'Add Gallery Type',
                        onClick: function (e) {
                            app.methods.url.redirectTo(pageUrls.admin.galleryManagement.addGalleryType);
                        }
                    }
                },{
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "search",
                        hint: 'Search data',
                        onClick: function (e) {
                            var $filterRow = $dxDataGrid.option('filterRow');
                            $dxDataGrid.option('filterRow', { visible: !$filterRow.visible });
                        }
                    }
                }, {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "filter",
                        hint: 'Filter data',
                        onClick: function (e) {
                            var $headerFilter = $dxDataGrid.option('headerFilter');
                            $dxDataGrid.option('headerFilter', { visible: !$headerFilter.visible });
                        }
                    }
                });
            }
        }).dxDataGrid('instance');
    }).catch(function (erro) {

    });

});

var deleteGalleryType = function (galleryTypeId) {
    app.methods.page.deleteConfirm(function () {
        galleryTypeId = parseInt(galleryTypeId);
        if (!isNaN(galleryTypeId) && galleryTypeId > 0) {
            var $param= {
                galleryTypeId: galleryTypeId
            };
            api.http.post(apiURLs.admin.galleryManagement.deleteGalleryType, $param).then(function (data) {
                api.successMsgHandler(data);
                location.reload(true);
            }).catch(api.errorHandler);

        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, galleryTypeId);
};

var blockUnblockUser = function (userId, chk) {
    let $isChecked = $(chk).is(':checked');
    var $param = {
        userId: userId,
        IsBlocked: $isChecked
    };
    api.http.post(apiURLs.admin.userManagement.blockUser, $param).then(function (data) {
        api.successMsgHandler(data);
        app.methods.msg.setMsgQueue(data.data.message);
        app.methods.url.reloadPage();
    }).catch(api.errorHandler);
};