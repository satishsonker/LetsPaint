


const apiGet = function (url, success) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            type: app.api.methodType.get,
            url: apiURLs.baseUrl + url,
            contentType: 'application/json',
            dataType: 'json',
            success: function (data, responseText, jqXHR) {
                resolve(data);
            },
            error: function (z,y,z) {
                reject(z, y, z);
                apiError(z, y, z);
        }
        });
    });
}

const apiPost = function (url, params, options) {
 
    return new Promise(function (resolve, reject) {
        let option = {};
        option.url = apiURLs.baseUrl + url;
        option.type = app.api.methodType.post;
        option.timeout = 300000; 
        option.contentType= "application/json";
        option.data = JSON.stringify(params);
            option.success = function (data, responseText, jqXHR) {
                resolve(data);
            };
        option.error = function (data) {
            reject(data);
        };

        $.extend(option, options);
        $.ajax(option);
    });
};

const apiPostWithFile = function (url, params, options) {
    return new Promise(function (resolve, reject) {
        let option = {};
        option.url = apiURLs.baseUrl + url;
        option.type = app.api.methodType.post;
        option.processData = false;
        option.contentType = false;
        option.data = params;
        option.beforeSend = function (xhr) {
            xhr.setRequestHeader('authorization', '');
        },//app.common.token); },
        option.success = function (data, res, jqXHR) {
                resolve(data);
                //var $responseData = jqXHR.getResponseHeader("refreshToken");
                //if ($responseData !== null && $responseData.indexOf("Token") > -1) {
                //    $responseData = JSON.parse(jqXHR.getResponseHeader("refreshToken"));
                //    app.common.token = $responseData.Token;
                //}
            };
        option.error = function (data) {
            reject(data);
        };

        $.extend(option, options);
        $.ajax(option);
    });
};

const apiError = function (x, y, z) {
    console.log(x);
    console.log(y);
    console.log(z);
}

const api = {
    http: {
        get: apiGet,
        post: apiPost,
        postWithFile: apiPostWithFile
    }
};
