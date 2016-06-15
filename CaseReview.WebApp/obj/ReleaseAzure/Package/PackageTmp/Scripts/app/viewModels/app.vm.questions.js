function Question(
    id,
    sectionID,
    questionName,
    displayOrder,
    isActive,
    isMandatory,
    risk
    ) {
    var self = this;
    self.ID = ko.observable(id);
    self.SectionID = ko.observable(sectionID);
    self.DisplayOrder = ko.observable(displayOrder);
    self.IsActive = ko.observable(isActive);
    self.IsMandatory = ko.observable(isMandatory);
    self.Risk = ko.observable(risk);

    self.QuestionName = ko.observable(questionName).extend({
        confirmable: {
            message: "Are you really sure you want to update this? This will affect all existing answers related to this record!", callback:
                function () {
                    update(self);
                }
        }
    });

    self.IsActive.subscribe(
        function (newValue) {
            update(self);
        }
    );
    self.IsMandatory.subscribe(
        function (newValue) {
            update(self);
        }
    );
    self.Risk.subscribe(
        function (newValue) {
            update(self);
        }
    );


}

$(function () {
    var ld = localData;
    var vm = app.viewmodel;
    vm.Sections = ko.mapping.fromJS(ld);

    vm.Sections().forEach(function (section) {        

        section.Questions().forEach(function (question) {
        
            question.QuestionName = ko.observable(question.QuestionName()).extend({
                confirmable: {
                    message: "Are you really sure you want to update this? This will affect all existing answers related to this record!", callback:
                        function () {
                            update(question);
                        }
                }
            });

            question.IsActive.subscribe(
                function (newValue) {
                    update(question);
                }
            );

            question.IsMandatory.subscribe(
                function (newValue) {
                    update(question);
                }
            );
            question.Risk.subscribe(
                function (newValue) {
                    update(question);
                }
            );

        });
    });

    
    vm.SelectedSection = ko.observable();

    vm.SelectedSection.subscribe(
        function () {
            setTimeout(app.view.setUpSortable, 1000);
        }
    );


    vm.isEditMode = ko.observable(false);
    vm.Risks = [
         { text: 'Not set', value: null }
        , { text: 'High', value: 3 }
        , { text: 'Medium', value: 2 }
        , { text: 'Low', value: 1 }
    ];
    vm.MandatoryOptions = [
         { text: 'Not set', value: null }
        , { text: 'M', value: true }
        , { text: 'NM', value: false }
    ];
    vm.swapEditMode = function () {
        if (vm.isEditMode()) {
            vm.isEditMode(false);
        }
        else {
            vm.isEditMode(true);
        }
    };

    vm.add = function (p) {
        var arrQuestions = vm.SelectedSection().Questions();
        arrQuestions.push(
            new Question(
            p.ID,
            p.SectionID,
            p.QuestionName,
            p.DisplayOrder,
            p.IsActive,
            p.IsMandatory,
            p.Risk)
        );
        vm.SelectedSection().Questions(arrQuestions);
    };

});

