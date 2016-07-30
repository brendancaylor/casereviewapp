function replaceAll(string, find, replace) {
    return string.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}


function escapeRegExp(string) {
    return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

var Answer = function (data, sectionID) {
    this.ID = data.ID;
    this.SectionID = sectionID;
    this.CaseReviewWorkSheetID = data.CaseReviewWorkSheetID;
    this.Comments = ko.observable(data.Comments);
    
    this.CommentsFormatted = ko.computed(function () {
        var result = this.Comments();
        if (result != "") {
            //debugger;
        }
        var formatedComments = replaceAll(result, "\n", "<br />");
        formatedComments = replaceAll(formatedComments, "\r", "<br />");
        return formatedComments;
    }, this);
    this.IsActive = ko.observable(false);


    this.Compliant = ko.observable("");
    if (data.Compliant != null) {
        this.Compliant("0");
        if (data.Compliant) {
            this.Compliant("1");
        }
    }
    this.CompliantText = ko.computed(function () {
        var result = "";
        if (this.Compliant() != "") {
            result = "No";
            if (this.Compliant() == "1") {
                result = "Yes";
            }
        }
        return result;
    }, this);

    this.QuestionName = data.Question.QuestionName;
    this.IsMandatory = "";
    if (data.Question.IsMandatory) {
        this.IsMandatory = data.Question.IsMandatory == true ? "Mandatory" : "NM";
    }
    this.Risk = "";
    if (data.Question.Risk) {
        switch (data.Question.Risk) {
            case 1:
                this.Risk = "Low";
                break;
            case 2:
                this.Risk = "Medium";
                break;
            case 3:
                this.Risk = "High";
                break;
        }
    }
};

var Section = function (data) {

    this.ID = data.ID;
    this.SectionName = data.SectionName;
    this.DisplayOrder = data.DisplayOrder;
    this.IsActive = ko.observable(false);
    this.Answers = ko.observableArray();
    var arrAnswers = [];
    data.Answers.forEach(
        function (answer) {
            arrAnswers.push(new Answer(answer, data.ID));
        }
    );
    this.Answers(arrAnswers);
};

var dataMappingOptions = {
    key: function (data) {
        return data.ID;
    },
    create: function (options) {
        return new Section(options.data);
    }
};

$(function () {
    var ld = localData;
    var vm = app.viewmodel;
    vm.Sections = ko.mapping.fromJS(ld, dataMappingOptions);
    vm.Sections()[0].IsActive(true);
    vm.SelectedSectionID = ko.observable(vm.Sections()[0].ID);
    vm.ModalAnswerQuestion = ko.observable("");
    vm.ModalAnswerQuestionIDs = ko.observable("");
    vm.ModalAnswerComments = ko.observable("");
    vm.ModalAnswerCompliant = ko.observable("");
    vm.showAnswerModal = function (answer) {
        //debugger;
        app.view.showModalForSingleAnswer(answer);
    };

    vm.selectSection = function (section) {
        //debugger;
        vm.SelectedSectionID(section.ID);
    };
    
});