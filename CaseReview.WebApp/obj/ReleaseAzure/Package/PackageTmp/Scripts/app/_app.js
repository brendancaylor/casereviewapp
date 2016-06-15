/*
 * app javascript module.
 * Define all app javascript inside this module to avoid namespace clashes.
 */
(function () {
    "use strict";

    window.app = window.app || {};                  // top level parent module
    window.app.helpers = {};                        // global helper functions
    window.app.api = {};                            // calls to the network REST api
    window.app.view = {};                           // all view related functions
    window.app.security = {};                       // security
    window.app.models = {};                         // JSON transportation
    window.app.constants = {};                      // constants, served by /Script/Constants
}());


app.doNothing = function() {
    return false;
};

// browser safe console logging, only log if console exists
// ALL logging should use this mechanism
app.helpers.log = function (obj) {
    if (typeof console != "undefined") {
        if (typeof obj == "string") {
            console.log(obj);
            return obj;
        } else {
            var fmt = JSON.stringify(obj);
            console.log(fmt);
            return fmt;
        }
    }

    return "";
};

// stuff that is common to ALL app pages
app.initialisePage = function () {

    $(document).ready(function () {
        if ($.validator) {
            $.validator.methods.date = function (value, element) {
                return this.optional(element) || parseDate(value, "dd/MM/yyyy") !== null;
            };
        }
    });

};

app.api.callApi = function (data, url, isAsync, callback, callbackError) {
    if (url.charAt(0) == '/') {
        url = url.substring(1);
    }

    return $.ajax({
        //url: app.helpers.getPathToRoot() + url,
        url: app.helpers.pathToRoot + url,
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        success: function (data, success) {
            //app.helpers.log(data);
            callback(data);
        },
        error: function (error) {
            if (callbackError) {
                callbackError(error);
            } else {
                app.helpers.log(error);
                app.helpers.showErrorNotification(error.statusText);
            }
        },
        async: isAsync
    });
};

