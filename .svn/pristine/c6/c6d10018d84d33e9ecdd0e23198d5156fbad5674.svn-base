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
                
                $("#sortable .sortableLi").each(function(i, el) {
                    if (i > 0) {
                        data += ",";
                    }
                    var id = $(el).attr('id');

                    var match = ko.utils.arrayFirst(app.viewmodel.BaseModels(), function (item) {
                        return id === item.ID();
                    });
                    if (match) {
                        match.DisplayOrder(i);
                    }

                    data += "'" + id + "'";
                });
                data += "]";
                reorderBaseModels(data);
            }
        }
  );

    $("#sortable").disableSelection();

    $('#inpAddBaseModel').keypress(function (event) {
        if (event.keyCode == 13) {
            addNewBaseModel();
        }
    });

});

function reorderBaseModels(ids) {

    app.api.callApi(ids, urlApiReorder, true,
        function (callbackData) {
            // success
            app.viewmodel.forceUpdate("test");
            app.helpers.showSavedNotification("Reordered :-)");
        },

        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
        }

    );
}

function updateBaseModel(obj, propBeingUpdated) {

    var data = {
        ID: obj.ID(),
        Name: obj.Name(),
        IsActive: obj.IsActive()
    };

    app.api.callApi(data, urlApiUpdate, true,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.helpers.showSavedNotification("Updated :-)");
        },

        function (callback) {
            // error

            app.helpers.showErrorNotification("ooops :-(");
        }

    );
}

function addNewBaseModel() {
    var newBaseModel = $("#inpAddBaseModel").val();
    if (newBaseModel == "") {
        return;
    }
    var data = {
        Name: newBaseModel,
        IsActive: true,
        DisplayOrder: app.viewmodel.BaseModels().length + 1
    };

    app.api.callApi(data, urlApiAdd, true,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.viewmodel.add(obj);
            app.helpers.showSavedNotification("Added :-)");
            $("#inpAddBaseModel").val("");
        }, 
        
        function (callback) {
            // error

            app.helpers.showErrorNotification("ooops :-(");
        }
        
    );
}
