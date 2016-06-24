function Staff(
    id,
    staffFirstname,
    staffSurname,
    isActive
    ) {
    var self = this;
    self.ID = ko.observable(id);
    self.StaffFirstname = ko.observable(staffFirstname);
    self.StaffSurname = ko.observable(staffSurname);
    self.IsActive = ko.observable(isActive);

    self.IsActive.subscribe(
        function (newValue) {
            app.view.update(self);
        }
    );
    self.StaffFirstname.subscribe(
        function (newValue) {
            app.view.update(self);
        }
    );
    self.StaffSurname.subscribe(
        function (newValue) {
            app.view.update(self);
        }
    );
}

$(function () {
    var ld = localData;
    var vm = app.viewmodel;
    vm.Staffs = ko.mapping.fromJS(ld);
    vm.AddButtonDisabled = ko.observable(false);
    
    vm.Staffs().forEach(function (obj) {        

        obj.IsActive.subscribe(
                function (newValue) {
                    app.view.update(obj);
                }
            );

        obj.StaffFirstname.subscribe(
                function (newValue) {
                    app.view.update(obj);
                }
            );

        obj.StaffSurname.subscribe(
                function (newValue) {
                    app.view.update(obj);
                }
            );

    });

    vm.isEditMode = ko.observable(false);
    vm.swapEditMode = function () {
        if (vm.isEditMode()) {
            vm.isEditMode(false);
        }
        else {
            vm.isEditMode(true);
        }
    };

    vm.add = function (p) {
        var arr = vm.Staffs();
        arr.push(
            new Staff(
            p.ID,
            p.StaffFirstname,
            p.StaffSurname,
            p.IsActive)
        );
        vm.Staffs(arr);
    };

});

