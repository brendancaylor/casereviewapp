$(function () {
    $('#StandardLine').select2();
    //jsonStaffNonCompliantTotals
});


app.view.showModalForAnswer = function (answer) {
    answer.IsActive(true);
    app.viewmodel.TotalsLoading(true);
    app.viewmodel.ModalAnswerID(answer.Id);
    app.viewmodel.ModalAnswerQuestionName(answer.QuestionName);
    app.viewmodel.ModalAnswerComments(answer.Comments());
    app.viewmodel.ModalAnswerFeedback(answer.Feedback());
    app.viewmodel.ModalAnswerFeedbackType(answer.FeedbackType());
    app.viewmodel.ModalAnswerIsMandatory(answer.IsMandatory);
    app.viewmodel.ModalAnswerRisk(answer.Risk);
    var data = 
        {
            StaffID: answer.StaffID,
            QuestionID: answer.QuestionID
        };

    app.api.callApi(data, "/api/NonCompliantTotals/getTotalsByStaffId?r=1", true,
        function (callbackData) {
            // success
            callbackData = JSON.parse(callbackData);

            app.viewmodel.Totals(callbackData);
            app.helpers.log(callbackData);
            app.viewmodel.TotalsLoading(false);
        },
        function (error) {
            app.viewmodel.TotalsLoading(false);
            app.helpers.showErrorNotification("ooops :-(");
        }
    );

    $('#myModal').modal('show');
};

$("#StandardLine").change(function (event, a, b) {
    //debugger;
    var constSelect = "- Select to add -";
    var val = $("#StandardLine").val();
    if (val && val != constSelect) {
        var existingComments = app.viewmodel.ModalAnswerFeedback();
        if (existingComments == null) {
            existingComments = "";
        }
        if (existingComments != "") {
            existingComments += '\n';
        }
        app.viewmodel.ModalAnswerFeedback(existingComments + val);
        $("#StandardLine").select2("val", "");
    }
});

app.view.save = function () {
    
    $("#saveButton").attr('disabled', 'disabled');
    $("#saveButton").addClass("disabled");
    app.viewmodel.Answers().forEach(
        function (answer) {
        	if (answer.Id == app.viewmodel.ModalAnswerID()) {
        		answer.Feedback(app.viewmodel.ModalAnswerFeedback());
        		answer.FeedbackType(app.viewmodel.ModalAnswerFeedbackType());
            }
        }
    );

    
    var data = {
    	ID: app.viewmodel.ModalAnswerID(),
        Feedback: app.viewmodel.ModalAnswerFeedback(),
        FeedbackType: app.viewmodel.ModalAnswerFeedbackType()
    };

    app.api.callApi(data, "/api/saveanswer/savefeedback", true,
        function (callbackData) {
            // success
            if (callbackData == "worked") {
                app.helpers.showSavedNotification("Saved :-)");
            }
            else {
                app.helpers.showErrorNotification("ooops :-( " + callbackData);
            }
            
            $("#saveButton").removeAttr('disabled');
            $("#saveButton").removeClass("disabled");
            $('#myModal').modal('hide');
        },
        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
            $("#saveButton").attr('disabled', 'false');
            $("#saveButton").removeClass("disabled");
            $('#myModal').modal('hide');
        }

    );


}

