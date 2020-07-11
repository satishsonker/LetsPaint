$(document).ready(function () {
    let $dxDataGrid;
    var $param = {
        pageNo: 1,
        pageSize:10
    };
    api.http.post(apiURLs.admin.userManagement.getUserList, $param).then(function (data) {
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
                                    <li title="Edit"><a href="/admin/UserManagement/AddUser?userid=${options.data.userId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteUser(${options.data.userId})"><i class="la la-trash"></i></li>
                                    <li title="${(options.data.isBlocked ? 'User is Blocked' : 'User is not Blocked')} "><label class="switch"><input class="attr-isactive" type="checkbox" onclick="blockUnblockUser(${options.data.userId},this)"  ${(options.data.isBlocked ? 'checked="checked"' : '')} ><span class="slider round"></span></label></li>
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
                    caption: "Photo",
                    width: 70,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        var $photoSrc = options.data.photo;
                        if (options.data.photo === null) {
                            $photoSrc = options.data.gender === 'Male' ? '/images/male_icon.png' : '/images/female_icon.png';
                        }
                        $(`<div>`)
                            .append(`<img style="width: 50px;height: 50px;border-radius: 50px;" src="${$photoSrc}" />`)
                            .appendTo(container);
                    }
                }, "email", "firstName", "lastName","gender", "mobile","userType"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "fa fa-user-astronaut",
                        hint: 'Add Astrologer',
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.addAstrologer);
                        }
                    }
                }, {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        hint: 'Add User',
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.addUser);
                        }
                    }
                }, {
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

var deleteUser = function (userId) {
    app.methods.page.deleteConfirm(function () {
        userId = parseInt(userId);
        if (!isNaN(userId) && userId > 0) {
            api.http.post(apiURLs.admin.userManagement.deleteUser + `?userid=${userId}`).then(function (data) {
                api.successMsgHandler(data);
                location.reload(true);
            }).catch(api.errorHandler);

        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, userId);
};

var blockUnblockUser = function (userId,chk) {
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