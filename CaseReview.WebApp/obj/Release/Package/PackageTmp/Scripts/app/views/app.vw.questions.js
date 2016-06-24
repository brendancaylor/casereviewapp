$(function () {

    app.viewmodel.isBound(true);
    
    app.view.SortByDisplayOrder = function (a, b) {
        var aDisplayOrder = a.DisplayOrder();
        var bDisplayOrder = b.DisplayOrder();
        return ((aDisplayOrder < bDisplayOrder) ? -1 : ((aDisplayOrder > bDisplayOrder) ? 1 : 0));
    };

    app.view.setUpSortable = function () {

        $(".sortable").sortable(

            {
                stop: function (event, ui) {
                    //debugger;
                    var arr = app.viewmodel.SelectedSection().Questions();
                    var data = "[";

                    $(".sortable .sortableLi").each(function (i, el) {
                        if (i > 0) {
                            data += ",";
                        }
                        var id = $(el).attr('id');

                        var match = ko.utils.arrayFirst(app.viewmodel.SelectedSection().Questions(), function (item) {
                            return id === item.ID();
                        });
                        if (match) {
                            match.DisplayOrder(i);
                        }

                        data += "'" + id + "'";
                    });
                    data += "]";

                    arr.sort(app.view.SortByDisplayOrder);
                    app.viewmodel.SelectedSection().Questions(arr);
                    app.view.reorder(data);
                }
            }
      );

    };

    $("#sortable").disableSelection();

    $('#inpAdd').keypress(function (event) {
        if (event.keyCode == 13) {
            if (!app.viewmodel.AddButtonDisabled()) {
                addNew();
            }
        }
    });

    app.view.setUpSortable()
});

app.view.reorder = function (ids) {

    app.api.callApi(ids, urlApiReorder, true,
        function (callbackData) {
            // success
            //app.viewmodel.forceUpdate("test");
            app.helpers.showSavedNotification("Reordered :-)");
        },

        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
        }

    );
}

function update(obj, propBeingUpdated) {

    var data = {
        ID: obj.ID(),
        SectionID: obj.SectionID(),
        QuestionName: obj.QuestionName(),
        DisplayOrder: obj.DisplayOrder(),
        IsActive: obj.IsActive(),
        IsMandatory: obj.IsMandatory(),
        Risk: obj.Risk()
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

function addNew() {
    var vm = app.viewmodel;
    vm.AddButtonDisabled(true);
    var newItem = $("#inpAdd").val();
    if (newItem == "") {
        return;
    }
    var data = {
        QuestionName: newItem,
        IsActive: true,
        DisplayOrder: app.viewmodel.SelectedSection().Questions().length,
        IsMandatory: null,
        Risk: null,
        SectionID: app.viewmodel.SelectedSection().ID()
    };
    /*
    ID = Guid.Empty,
                IsMandatory = model.IsMandatory,
                Risk = model.Risk,
                SectionID = model.SectionID
                */

    app.api.callApi(data, urlApiAdd, true,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.viewmodel.add(obj);
            app.helpers.showSavedNotification("Added :-)");
            $("#inpAdd").val("");
            vm.AddButtonDisabled(false);
        },

        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
            vm.AddButtonDisabled(false);
        }

    );
}
