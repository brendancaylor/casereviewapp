$(function () {

    app.viewmodel.isBound(true);
    var ld = localData;
    app.viewmodel.bind(ld);
    /*
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));

    elems.forEach(function(html) {
        var switchery = new Switchery(html, {
            size: 'small'
        });
    });
    */

    $("#sortable").sortable(

        {
            stop: function(event, ui) {
                debugger;

                var data = "[";
                
                $("#sortable li").each(function(i, el) {
                    if (i > 0) {
                        data += ",";
                    }
                    var id = parseInt($(el).attr('id'));

                    var match = ko.utils.arrayFirst(app.viewmodel.CaseReviewTypes(), function (item) {
                        return id === item.ID();
                    });
                    if (match) {
                        match.DisplayOrder(i);
                    }

                    data += id;
                });
                data += "]";
                reorderCaseTypes(data);
            }
        }
  );

    $("#sortable").disableSelection();

    $('#inpAddCaseType').keypress(function (event) {
        if (event.keyCode == 13) {
            addNewCaseType();
        }
    });

});

function reorderCaseTypes(ids) {

    app.api.callApi(ids, "api/admin/reorderCaseTypes", false,
        function (callbackData) {
            // success
            app.viewmodel.forceUpdate("test");
            app.helpers.showSavedNotification("Reordered :-)");
        },

        function (callback) {
            // error
            app.helpers.showSavedNotification("ooops :-(");
        }

    );
}

function updateCaseType(newvalue, obj) {

    var data = {
        ID: obj.ID(),
        IsActive: newvalue
    };

    app.api.callApi(data, "api/admin/updateCaseType", false,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.helpers.showSavedNotification("Updated :-)");
        },

        function (callback) {
            // error

            app.helpers.showSavedNotification("ooops :-(");
        }

    );
}

function addNewCaseType() {
    var newCaseType = $("#inpAddCaseType").val();
    var data = {
        TypeName: newCaseType,
        IsActive: true,
        DisplayOrder: app.viewmodel.CaseReviewTypes().length + 1
    };

    app.api.callApi(data, "api/admin/addCaseType", false,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.viewmodel.add(obj);
            app.helpers.showSavedNotification("Added :-)");
            $("#inpAddCaseType").val("");
        }, 
        
        function (callback) {
            // error

            app.helpers.showSavedNotification("ooops :-(");
        }
        
    );
}
