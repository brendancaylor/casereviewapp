function replaceAll(string, find, replace) {
    if (string){
        return string.replace(new RegExp(escapeRegExp(find), 'g'), replace);
    }
    else {
        return "";
    }
}


function escapeRegExp(string) {
    return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

var Answer = function (data, sectionID) {

    var test = {
        "Comments": "A client review was carried out in June. There was only 1 contact attempt made to the client to ascertain if there were any changes to the clients circumstances.\nPlease make further attempts to contact the client so that we can update them with the running of their plan and ensure that we are in still able to offer them the best possible resolution for their situation"
        , "StaffSurname": "Bean", "SectionOrder": 0, "QuestionOrder": 1
    };
    this.Id = data.Id;
    this.StaffSurname = data.StaffSurname;
    this.ClientRef = data.ClientRef;
    this.Date = data.Month + "/" + data.Year;
    this.SectionName = data.SectionName;
    this.QuestionID = data.QuestionID;
    this.QuestionName = data.QuestionName;

    this.IsMandatory = "";
    if (data.IsMandatory) {
        this.IsMandatory = data.IsMandatory == true ? "Mandatory" : "NM";
    }
    this.Risk = "";
    if (data.Risk) {
        switch (data.Risk) {
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

    this.StaffID = data.StaffID;

    this.Comments = ko.observable(data.Comments);
    this.Feedback = ko.observable(data.Feedback);
    this.FeedbackType = ko.observable(data.FeedbackType);
    this.CamConfirmation = ko.observable(data.CamConfirmation);

    this.IsActive = ko.observable(false);

    this.CommentsFormatted = ko.computed(function () {
        var result = this.Comments();
        if (result != "") {
            //debugger;
        }
        var formatedComments = replaceAll(result, "\n", "<br />");
        formatedComments = replaceAll(formatedComments, "\r", "<br />");
        return formatedComments;
    }, this);

    this.FeedbackFormatted = ko.computed(function () {
        var result = this.Feedback();
        if (result != "") {
            //debugger;
        }
        var formatedFeedback = replaceAll(result, "\n", "<br />");
        formatedFeedback = replaceAll(formatedFeedback, "\r", "<br />");
        return formatedFeedback;
    }, this);

};

var dataMappingOptions = {
    key: function (data) {
        return data.ID;
    },
    create: function (options) {
        return new Answer(options.data);
    }
};

$(function () {
    var ld = localData;
    var vm = app.viewmodel;
    vm.Answers = ko.mapping.fromJS(ld, dataMappingOptions);

    vm.ModalAnswerID = ko.observable("");
    vm.ModalAnswerQuestionName = ko.observable("");
    vm.ModalAnswerComments = ko.observable("");
    vm.ModalAnswerFeedback = ko.observable("");
    vm.ModalAnswerFeedbackType = ko.observable("");
    vm.ModalAnswerIsMandatory = ko.observable("");
    vm.ModalAnswerRisk = ko.observable("");

    vm.TotalsLoading = ko.observable(false);
    vm.Totals = ko.observable();
    vm.showAnswerModal = function (answer) {
        //debugger;
        app.view.showModalForAnswer(answer);
    };

});