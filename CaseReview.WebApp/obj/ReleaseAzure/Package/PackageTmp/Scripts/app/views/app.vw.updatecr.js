$(function () {
    $('#StandardID').select2();
});



app.view.selectAll = function () {
    app.view.deselectAll();
    app.viewmodel.Sections().forEach(
        function (section) {
            if (section.ID == app.viewmodel.SelectedSectionID()) {    
                section.Answers().forEach(
                    function (answer) {
                        answer.IsActive(true);
                    }
                );
            }
        }
    );
};

app.view.deselectAll = function () {
    app.viewmodel.Sections().forEach(
            function (section) {
                section.Answers().forEach(
                    function (answer) {
                        answer.IsActive(false);
                    }
                );
            }
        );
};

app.view.selectUnanswered = function () {
    app.view.deselectAll();
    app.viewmodel.Sections().forEach(
            function (section) {
                if (section.ID == app.viewmodel.SelectedSectionID()) {
                    section.Answers().forEach(
                        function (answer) {
                            if (answer.Compliant() == ""){
                                answer.IsActive(true);
                            }
                        }
                    );
                }
            }
        );
};

app.view.showModalForSingleAnswer = function (answer) {
    app.view.deselectAll();
    answer.IsActive(true);
    var ids = [answer.ID];
    app.view.showModalCommon(ids, answer.QuestionName, answer.Comments(), answer.Compliant());
};

app.view.showModalForManyAnswers = function () {
    var selectedAnswers = [];
    var selectedAnswerIds = [];
    app.viewmodel.Sections().forEach(
        function (section) {
            section.Answers().forEach(
                function (answer) {
                    if (answer.IsActive()) {
                        selectedAnswerIds.push(answer.ID);
                        selectedAnswers.push(answer.QuestionName);
                    }
                }
            );
        }
    );
    var questionName = "Multiple Questions Selected!";
    if (selectedAnswers.length == 1) {
        questionName = selectedAnswers[0];
    }
    if (selectedAnswerIds.length > 0){
        app.view.showModalCommon(selectedAnswerIds, questionName, "", null);
    }
}

app.view.showModalCommon = function (ids, questionName, comments, compliant) {
    app.viewmodel.ModalAnswerQuestion(questionName);
    app.viewmodel.ModalAnswerQuestionIDs(ids);
    app.viewmodel.ModalAnswerComments(comments);
    app.viewmodel.ModalAnswerCompliant(compliant);
    $('#myModal').modal('show');
};

app.view.addLine = function (ddElement) {
    var dd = $(ddElement);
    var existingComments = app.viewmodel.ModalAnswerComments();
    if (dd.val() != "- Select to add -") {
        if (existingComments != "") {
            existingComments += '\n';
        }
        app.viewmodel.ModalAnswerComments(existingComments + dd.val());
    }
}

app.view.save = function () {
    var selectedAnswerIds = [];
    app.viewmodel.Sections().forEach(
        function (section) {
            section.Answers().forEach(
                function (answer) {
                    if (answer.IsActive()) {
                        selectedAnswerIds.push(answer.ID);
                        answer.Comments(app.viewmodel.ModalAnswerComments());
                        answer.Compliant(app.viewmodel.ModalAnswerCompliant());
                    }
                }
            );
        }
    );

    
    var data = {
        ids: selectedAnswerIds,
        comments: app.viewmodel.ModalAnswerComments(),
        compliant: app.viewmodel.ModalAnswerCompliant() == "" ? null : app.viewmodel.ModalAnswerCompliant() == "1" ? true : false
    };

    app.api.callApi(data, "/api/saveanswer/savemany", true,
        function (callbackData) {
            // success
            app.view.deselectAll();
            app.helpers.showSavedNotification("Saved :-)");
        },
        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
        }

    );


}

