$(function () {

    app.viewmodel.isBound(true);
    
    app.view.SortByStaffSurname = function (a, b) {
        var aStaffSurname = a.StaffSurname();
        var bStaffSurname = b.StaffSurname();
        return ((aStaffSurname < bStaffSurname) ? -1 : ((aStaffSurname > bStaffSurname) ? 1 : 0));
    };


    $('#inpAddinpAddStaffSurname').keypress(function (event) {
        if (event.keyCode == 13) {
            if (!app.viewmodel.AddButtonDisabled()) {
                addNew();
            }
        }
    });

});

//arr.sort(app.view.SortByDisplayOrder);
//app.viewmodel.SelectedSection().Questions(arr);
//app.view.reorder(data);

app.view.update = function (obj) {

    var data = {
        ID: obj.ID(),
        StaffFirstname: obj.StaffFirstname(),
        StaffSurname: obj.StaffSurname(),
        IsActive: obj.IsActive(),
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

app.view.addNew = function () {
    var vm = app.viewmodel;
    vm.AddButtonDisabled(true);
    var staffFirstname = $("#inpAddStaffFirstname").val();
    var staffSurname = $("#inpAddStaffSurname").val();

    if (staffFirstname == "") {
        return;
    }
    var data = {
        IsActive: true,
        StaffFirstname: staffFirstname,
        StaffSurname: staffSurname,
    };

    app.api.callApi(data, urlApiAdd, true,
        function (callbackData) {
            // success
            var obj = jQuery.parseJSON(callbackData);
            app.viewmodel.add(obj);
            app.helpers.showSavedNotification("Added :-)");
            $("#inpAddStaffSurname").val("");
            $("#inpAddStaffFirstname").val("");
            vm.AddButtonDisabled(false);
        },

        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
            vm.AddButtonDisabled(false);
        }

    );
}
