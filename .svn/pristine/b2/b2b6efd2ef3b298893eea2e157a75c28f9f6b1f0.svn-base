function CaseReviewType(
    id,
    typeName,
    displayOrder,
    isActive
    ) {
    var self = this;
    self.ID = ko.observable(id);
    self.TypeName = ko.observable(typeName);
    self.DisplayOrder = ko.observable(displayOrder);
    self.IsActive = ko.observable(isActive);

    self.IsActive.subscribe(
        function (newValue) {
            updateCaseType(newValue, self);
        }
    );

}

$(function() {
    var vm = app.viewmodel;

    // properties
    vm.forceUpdate = ko.observable("");
    vm.isEditMode = ko.observable(false);
    vm.CaseReviewTypes = ko.observableArray();

    vm.SortedCaseReviewTypes = ko.computed(function () {
        var test = vm.forceUpdate();
        return vm.CaseReviewTypes().sort(function (left, right) {
            return left.DisplayOrder() == right.DisplayOrder() ?
                 0 :
                 (left.DisplayOrder() < right.DisplayOrder() ? -1 : 1);
        });
    });

    // methods

    vm.add = function (p) {
        var arrCaseReviewTypes = vm.CaseReviewTypes();
        arrCaseReviewTypes.push(
            new CaseReviewType(
            p.ID,
            p.TypeName,
            p.DisplayOrder,
            p.IsActive)
        );
        vm.CaseReviewTypes(arrCaseReviewTypes);
    };

    vm.bind = function(caseReviewTypes) {
        if(caseReviewTypes){
            var arrCaseReviewTypes = [];
            jQuery.each(caseReviewTypes, function(i) {
                var p = this;
                arrCaseReviewTypes.push(

                    new CaseReviewType(
                    p.ID,
                    p.TypeName,
                    p.DisplayOrder,
                    p.IsActive)
                );
            });            
            vm.CaseReviewTypes(arrCaseReviewTypes);
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