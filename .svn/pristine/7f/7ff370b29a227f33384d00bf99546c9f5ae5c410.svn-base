// Generate a GUID
app.helpers.generateUUID = function () {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};

app.helpers.showModalAlert = function (title, body) {
	$('#modalAlertTitle').html(title);
    $('#modalAlertTitleBody').html(body);
    $('#modalAlert').modal({ show: true, backdrop: 'static' });
};

app.helpers.showModalError = function (title, body) {
	$('#modalAlertTitle').html('<span class="glyphicon glyphicon-exclamation-sign field-validation-error"></span><span style="margin-left: 0.5em;">' + title + '</span>');
    $('#modalAlertTitleBody').html(body);
    $('#modalAlert').modal({ show: true, backdrop: 'static' });
};

app.helpers.showSavedNotification = function (message) {

    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000"
    };

    toastr.success(message);
    document.body.style.cursor = 'default';

	// http://codeseven.github.io/toastr/demo.html
};

app.helpers.showErrorNotification = function (message) {

    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "2000",
        "timeOut": "5000"
    };

    toastr.error(message);
    document.body.style.cursor = 'default';

    // http://codeseven.github.io/toastr/demo.html
};

app.helpers.showWarningNotification = function (message) {

    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "2000",
        "timeOut": "5000"
    };

    toastr.warning(message);
    document.body.style.cursor = 'default';

};



app.helpers.getHashObject = function () {
    if (window.location.hash && window.location.hash.length > 0) {
        return jQuery.parseJSON(window.location.hash.substring(1));
    } else {
        return {};
    }
};

app.helpers.getPathToRoot = function () {
    return window.stc_app_root || window.location.protocol + '//' + window.location.host + '/';
};


app.helpers.calcHMAC = function(hmacText) {
    try {

        var hmacTextType = "TEXT";
        var hmacKeyInput = "REPLACEME";
        var hmacKeyInputType = "TEXT";
        var hmacVariant = "SHA-256";
        var hmacOutputType = "B64";
        var hmacObj = new jsSHA(hmacText, hmacTextType);

        return hmacObj.getHMAC(
            hmacKeyInput,
            hmacKeyInputType,
            hmacVariant,
            hmacOutputType
        );
    } catch(e) {
        return e;
    }
};

app.helpers.showSpinDisableButton = function(buttonId) {
    $(buttonId).prop('disabled', true);
    $(".fa-spin").show();
};

app.helpers.hideSpinEnableButton = function (buttonId) {
    $(buttonId).prop('disabled', false);
    $(".fa-spin").hide();
};

app.helpers.showPageLoading = function() {
	//$('#modalAlertTitle').html("NOT IMPLEMENTED");
	//$('#modalAlertTitleBody').html("NOT IMPLEMENTED");
	//$('#modalAlert').modal({ show: true, backdrop: 'static' });

    bootbox.dialog({
        message: "Please wait ...",
        className: "modal-info",
        closeButton: false,
        cancelButton: false,
        callback: function (result) {
            return false;
        }
    });
};

app.helpers.hidePageLoading = function() {
    //$('#modalAlert').modal('hide');

    bootbox.hideAll();

};


app.helpers.injectHtmlFromAjax = function(url, targetElementId, callback) {

    $.ajax({
        type: "GET",
        url: url,
        cache: false,
        success: function (result) {
            $("#" + targetElementId).html(result);
            if (callback && typeof (callback) == "function")
                callback(true);
        },
        error: function (req, status, error) {
            if (callback && typeof (callback) == "function")
                callback(false);
        }
    });
};

app.helpers.handleError = function(ajaxContext) {
    var response = ajaxContext.responseText;
    app.helpers.showModalError(response);
};

app.helpers.handleComplete = function(ajaxContext, status) {
    // do something
};

app.helpers.handleSuccess = function (ajaxContext, status) {
    // do something
};

app.helpers.copyObjectValues = function(src, tgt) {
    for (var key in src) {
        if (key in tgt) {
            tgt[key] = src[key];
        }
    }
};


app.helpers.formatPercent = function (value, places) {
    var dp = 1;
    if (places) {
        dp = places;
    }
    
    return value.toFixed(dp) + "%";
};

app.helpers.formatPlaces = function(value, places) {
    return value.toFixed(places);
};

app.helpers.getObjectClass = function(obj) {
    if (obj && obj.constructor && obj.constructor.toString) {
        var arr = obj.constructor.toString().match(
            /function\s*(\w+)/);

        if (arr && arr.length == 2) {
            return arr[1];
        }
    }

    return undefined;
};

app.helpers.roundOneDecimalPlace = function(value) {
    return Math.round(value * 10) / 10;
};

app.helpers.roundTwoDecimalPlaces = function (value) {
    return Math.round(value * 100) / 100;
};

app.helpers.roundThreeDecimalPlaces = function (value) {
    return Math.round(value * 1000) / 1000;
};

app.helpers.notNullOrEmpty = function(val) {

    if (typeof (val) === "undefined") {
        return false;
    }
    else if (val == null) {
        return false;
    }
    else if (val === "") {
        return false;
    } else {
        return true;
    }
};

app.helpers.delayedNavigate = function (url) {
    setTimeout(" window.location.replace(\"" + url + "\")", 100);
};

app.helpers.navigate = function(id, data, event, url) {
    window.location.replace(url +"/" + id);
};

app.helpers.newWindow = function (id, data, event, url) {
    window.open(url + "/" + id);
};



//has to be is this format DD/MM/YYYY
app.helpers.convertDateToUTC = function (stringDate) {
    if (stringDate && stringDate.length > 0)
        return moment.utc(stringDate, 'DD/MM/YYYY').toDate();
    else
        return null;
};


app.helpers.arrayBufferToBase64 = function(buffer) {
    if (!buffer) {
        return null;
    }
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
};

app.helpers.generateUUID = function () {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};

app.helpers.padZeros = function(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
};

app.helpers.isStringNegative = function(sNumber) {
    var result = false;
    if (parseFloat(sNumber)
        && parseFloat(sNumber) < 0) {
        result = true;
    }
    return result;
};

app.helpers.stringToRgb = function (sRgb, alterBy) {

    if (sRgb) {
        var strArray = sRgb.split(',');
        if (strArray.length == 3) {
            return { red: parseInt(strArray[0]) + alterBy, green: parseInt(strArray[1]) + alterBy, blue: parseInt(strArray[2]) + alterBy };
        }
    }
    return {
        red: 0,
        green: 0,
        blue: 0
    };
};

app.helpers.randomRGB = function () {

    var red = Math.floor(Math.random() * 255) + 1;
    var green = Math.floor(Math.random() * 255) + 1;
    var blue = Math.floor(Math.random() * 255) + 1;
    //var blue= 0;

    while (red + green + blue > 500) {
        red = Math.floor(Math.random() * 255) + 1;
        green = Math.floor(Math.random() * 255) + 1;
        //green = 0;
        blue = Math.floor(Math.random() * 255) + 1;
    }
    if (red + green + blue > 500) {
        alert('but why!!');
    }
    return {
        red: red,
        green: green,
        blue: blue
    };
};

app.helpers.rgbDiff = function (rbg1, rbg2) {
    return (
        (Math.abs(rbg1.red - rbg2.red))
            + (Math.abs(rbg1.green - rbg2.green))
            + (Math.abs(rbg1.blue - rbg2.blue))
    );
};

app.helpers.contrast = function(F, B) {
    F = String(F).match(/\d+/g),
        B = String(B).match(/\d+/g);
    var abs = Math.abs,
        BG = (B[0] * 299 + B[1] * 587 + B[2] * 114) / 1000,
        FG = (F[0] * 299 + F[1] * 587 + F[2] * 114) / 1000,
        bright = Math.round(Math.abs(BG - FG)),
        diff = abs(B[0] - F[0]) + abs(B[1] - F[1]) + abs(B[2] - F[2]);
    return [bright, diff];
};

app.helpers.saveFile1 = function (csvContent, filename) {
    var blob = new Blob([csvContent], {
        type: "text/csv;charset=utf-8;"});

    if (navigator.msSaveBlob) {
        // IE only
        navigator.msSaveBlob(blob, filename);
    }
    else {
        var tempLink = document.createElement('a');
        tempLink.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(csvContent));
        tempLink.setAttribute('download', filename);
        tempLink.click();
    }
};

app.helpers.saveFile = function(content, filename) {
    var blob = new Blob([content], {
        type: "text/csv;charset=utf-8;"
    });

    if (navigator.msSaveBlob) {
        // IE only
        navigator.msSaveBlob(blob, filename);
    } else {
        var tempLink = document.createElement('a');
        // Chrome can't handle data URIs > 2mb, so use blob URL instead!
        if (window.chrome && window.navigator.vendor === "Google Inc.") {
            // is Google chrome
            var blobUrl = URL.createObjectURL(blob);
            tempLink.setAttribute('href', blobUrl);
        } else {
            tempLink.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(content));
        }
        tempLink.setAttribute('download', filename);
        var evt = document.createEvent('MouseEvents');
        evt.initMouseEvent('click', true, true, document.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
        tempLink.dispatchEvent(evt);

        // Clear up memory usage
        if (blobUrl) URL.revokeObjectURL(blobUrl);
    }
};

app.helpers.Json2Csv = function (JSONData) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    var row = "";

    //This loop will extract the label from 1st index of on array
    for (var index in arrData[0]) {

        //Now convert each value to string and comma-seprated
        row += index + ',';
    }

    row = row.slice(0, -1);

    //append Label row with line break
    CSV += row + '\r\n';

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            var val = arrData[i][index];
            if (typeof val == 'string') {

                var find = "\"";
                var re = new RegExp(find, 'g');

                val = val.replace(re, "\"\"");

                //val = val.replace("\"", "\"\"");
            }
            //
            if (
                val != null
                && typeof val == 'string'
                && val.indexOf("/Date(") != -1) {
                val = new moment(val).format("DD/MM/YYYY hh:mm");
            }
            //arrData[i][index]
            row += '"' + val + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }
    return CSV;

}