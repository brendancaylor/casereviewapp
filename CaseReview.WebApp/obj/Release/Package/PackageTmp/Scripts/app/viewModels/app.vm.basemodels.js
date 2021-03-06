﻿function BaseModel(
    id,
    name,
    displayOrder,
    isActive
    ) {
    var self = this;
    self.ID = ko.observable(id);
    self.DisplayOrder = ko.observable(displayOrder);
    self.IsActive = ko.observable(isActive);
    self.Name = ko.observable(name).extend({
        confirmable: {
            message: "Are you really sure you want to update this? This will affect all existing answers related to this record!", callback:
                function () {
                    updateBaseModel(self);
                }
        }
    });

    self.IsActive.subscribe(
        function (newValue) {
            updateBaseModel(self);
        }
    );


}

$(function() {
    var vm = app.viewmodel;

    // properties
    vm.forceUpdate = ko.observable("");
    vm.isEditMode = ko.observable(false);
    vm.BaseModels = ko.observableArray();
    
    vm.AddButtonDisabled = ko.observable(false);

    vm.SortedBaseModels = ko.computed(function () {
        var test = vm.forceUpdate();
        return vm.BaseModels().sort(function (left, right) {
            return left.DisplayOrder() == right.DisplayOrder() ?
                 0 :
                 (left.DisplayOrder() < right.DisplayOrder() ? -1 : 1);
        });
    });

    // methods

    vm.add = function (p) {
        var arrBaseModels = vm.BaseModels();
        arrBaseModels.push(
            new BaseModel(
            p.ID,
            p.Name,
            p.DisplayOrder,
            p.IsActive)
        );
        vm.BaseModels(arrBaseModels);
    };

    vm.bind = function(baseModels) {
        if(baseModels){
            var arrBaseModels = [];
            jQuery.each(baseModels, function(i) {
                var p = this;
                arrBaseModels.push(

                    new BaseModel(
                    p.ID,
                    p.Name,
                    p.DisplayOrder,
                    p.IsActive)
                );
            });            
            vm.BaseModels(arrBaseModels);
        }
    };

    vm.swapEditMode = function () {
        if (vm.isEditMode()) {
            vm.isEditMode(false);
        }
        else {
            vm.isEditMode(true);
        }
    };

});