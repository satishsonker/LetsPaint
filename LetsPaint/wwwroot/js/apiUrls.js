var baseUrl = "";
if (location.host.indexOf('localhost') > -1) {
    baseUrl = "http://localhost:59928";
} else {
    baseUrl = "http://letspaint.somee.com";
}
const url = {
    root: {
        home: {},
        query: {
            sendQuery:'/query/sendQuery'
        }
    }
}