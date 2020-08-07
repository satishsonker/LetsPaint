﻿$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    let $txtTitle, $txtBadge, $txtDescription, $ddlArtist, $txtSize, $numAvlQty, $txtMedium, $txtSurface, $numPrice, $txtTags, $chkIsAvl, $chkHasArtistSign, $chkHasArtistCertificate;
    var $query = app.methods.url.urlSearchParams();

    $chkIsAvl = $("#chkIsAvl").dxCheckBox({
        value: true,
        width: 80,
        text: "Is Available"
    }).dxCheckBox('instance');

    $chkHasArtistSign = $("#chkHasArtistSign").dxCheckBox({
        value: true,
        width: 80,
        text: "Has Artist Sign"
    }).dxCheckBox('instance');

    $chkHasArtistCertificate = $("#chkHasArtistCertificate").dxCheckBox({
        value: true,
        width: 80,
        text: "Has Artist Certificate"
    }).dxCheckBox('instance');

    $txtTitle = $('#txtTitle').dxTextBox({
        value: "",
        maxLength:50,
        placeholder: "Enter image Title",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Image title is required."
        }]
    }).dxTextBox('instance');

    $txtSize = $('#txtSize').dxTextBox({
        value: "",
        maxLength: 50,
        placeholder: "Enter product size",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Product size is required."
        }]
    }).dxTextBox('instance');

  

    $txtSurface = $('#txtSurface').dxTextBox({
        value: "",
        maxLength: 200,
        placeholder: "Enter product surface",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Product surface is required."
        }]
    }).dxTextBox('instance');

    $numAvlQty = $('#numAvlQty').dxNumberBox({
        value: "",
        min:1,
        max: 1000,
        placeholder: "Enter available quantity",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Available quantity is required."
        }]
    }).dxNumberBox('instance');

    $numPrice = $('#numPrice').dxNumberBox({
        value: "",
        min: 1,
        max: 9999999,
        placeholder: "Enter product price",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Product price is required."
        }]
    }).dxNumberBox('instance');

    $txtBadge = $('#txtBadge').dxTextBox({
        value: "",
        maxLength: 20,
        placeholder: "Enter image badge",
        showClearButton: true
    }).dxTextBox('instance');

    $txtDescription = $('#txtDescription').dxTextArea({
        value: "",
        height:113,
        maxLength: 500,
        placeholder: "Enter image description",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Image description is required."
        }]
    }).dxTextArea('instance');
    api.http.get(apiURLs.admin.galleryManagement.getDropdownData + `?key=artist`).then(function (data) {
        $ddlArtist=  $("#ddlArtist").dxSelectBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data,
                key: "value"
            }),
            displayExpr: "text",
            valueExpr: "value",
            value: data[0].value,
        }).dxSelectBox('instance');
    });

    api.http.get(apiURLs.root.base.getReflookupData + `?key=Tags,ProductMedium,ProductSurface,Badge`).then(function (data) {
        $txtBadge = $('#txtBadge').dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'Badge'),
                key: "refId"
            }),
            displayExpr: "refValue",
            valueExpr: "refId", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxTagBox('instance');
        $txtTags = $("#txtTags").dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey ==='Tags'),
                key: "refId"
            }),
            displayExpr: "refValue",
            valueExpr: "refId", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxTagBox('instance');
        $txtMedium = $('#txtMedium').dxTagBox({
            //value: "",
            //maxLength: 200,
            //placeholder: "Enter product medium",
            //showClearButton: true 
            dataSource: new DevExpress.data.ArrayStore({
            data: data.filter(x => x.refKey === 'ProductMedium'),
            key: "refId"
        }),
            displayExpr: "refValue",
                valueExpr: "refId", acceptCustomValue: true,
                    onCustomItemCreating: function (args) {
                        var newValue = args.text,
                            component = args.component,
                            currentItems = component.option("items");
                        currentItems.unshift(newValue);
                        component.option("items", currentItems);
                        args.customItem = newValue;
                    },
        searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product medium is required."
            }]
        }).dxTagBox('instance');

        $txtSurface = $('#txtSurface').dxTagBox({
            //value: "",
            //maxLength: 200,
            //placeholder: "Enter product medium",
            //showClearButton: true 
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'ProductSurface'),
                key: "refId"
            }),
            displayExpr: "refValue",
            valueExpr: "refId", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product surface is required."
            }]
        }).dxTagBox('instance');
    });
    if ($query && $query.get('galleryid')) {
        var $param = {
            pageNo: 1,
            pageSize: 10,
            id: $query.get('galleryid')
        };
        api.http.post(apiURLs.admin.galleryManagement.getGallery, $param).then(function (data) {
            $numGridSize.option('value', data.data[0].gridSize);
            $txtBaseUrl.option('value', data.data[0].baseUrl);
            $txtGalleryType.option('value', data.data[0].galleryType);
            $txtBaseUrl.option('value', data.data[0].baseUrl);
            $btn.option('text', 'Update')
        });
    }


    var $btn = $("#btnSave").dxButton({
        icon: "save",
        type: "success",
        text: "Save",
        useSubmitBehavior: true,
        onClick: function () {
            if ($('.dx-validationsummary').children().length > 0) {
                $('#summary-container').show();
            }
        }
    }).dxButton('instance');



    $('#frmAddUser').on('submit', function (e) {

        var $formData = {};
        $formData.GalleryType= $txtGalleryType.option('value');
        $formData.GridSize=$numGridSize.option('value');
        $formData.BaseUrl=$txtBaseUrl.option('value');
        $formData.UserId = 2;
        if ($query) {
            $formData.galleryTypeId = $query.get('gallerytypeid');
            }
        var $url = $btn.option('text') === 'Update' ? apiURLs.admin.galleryManagement.updateGalleryType : apiURLs.admin.galleryManagement.saveGalleryType;
        api.http.post($url, $formData).then(function (data) {
            api.successMsgHandler(data);
                location.reload(true);
        }).catch(api.errorHandler);
        e.preventDefault();
    });
});