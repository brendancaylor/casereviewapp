function loadModal(trElement) {
    var tr = $(trElement);
    $("#answerId").val(trElement.id);
    $("#yes").prop("checked", false);
    $("#no").prop("checked", false);
    $("#notset").prop("checked", false);

    if (tr.children()[2].innerHTML.indexOf("yes") != -1) {
        $("#yes").prop("checked", true);
    }
    else if (tr.children()[2].innerHTML.indexOf("no") != -1) {
        $("#no").prop("checked", true);
    }
    else {
        $("#notset").prop("checked", true);
    }
    $("#StandardID").val("- Select to add -");
    $("#pQuestion").html(tr.children()[0].innerHTML);
    var comments = tr.children()[1].innerHTML;
    var formatedComments = replaceAll(comments, "\n<br>", "\n");
    formatedComments = replaceAll(formatedComments, "<br>", "\n");
    $("#Comments").val(formatedComments);
};

function addLine(ddElement) {
    var dd = $(ddElement);
    var existingComments = $("#Comments").val();
    if (dd.val() != "- Select to add -") {
        if (existingComments != "") {
            existingComments += '\n';
        }
        $("#Comments").val(existingComments + dd.val());
    }
}

function save() {
    var answerId = $("#answerId").val();
    var tr = $("#" + answerId);
    var td1 = tr.children()[1];
    var td2 = tr.children()[2];
    $(td1).html(replaceAll($("#Comments").val(), "\n", "<br>"));

    var compliantString = "-";
    var compliant = null;
    if ($('#no').is(':checked')) {
        compliantString = "no";
        compliant = false;
    }
    else if ($('#yes').is(':checked')) {
        compliantString = "yes";
        compliant = true;
    }
    $(td2).html(compliantString);

    var data = {
        id: answerId,
        comments: $("#Comments").val(),
        compliant: compliant
    };
    app.api.callApi(data, "api/saveanswer", false,
        function(callback) {
            app.helpers.showSavedNotification("Saved :-)");
        }, null);
}


function replaceAll(string, find, replace) {
    return string.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}

function escapeRegExp(string) {
    return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}
