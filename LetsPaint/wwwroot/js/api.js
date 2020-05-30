


const apiGet = function (url, success) {
    $.ajax({
        type: 'GET',
        url: baseUrl+url,
        contentType: 'application/json',
        dataType:'json',
        success: success,
        error: apiError
    });
}

const apiPost = function (url, params, options) {
 
    return new Promise(function (resolve, reject) {
        let option = {};
        option.url = baseUrl + url;
        option.type= "POST";
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

const apiError = function (x, y, z) {
    console.log(x);
    console.log(y);
    console.log(z);
}

const api = {
    http: {
        get: apiGet,
        post: apiPost
    }
};
