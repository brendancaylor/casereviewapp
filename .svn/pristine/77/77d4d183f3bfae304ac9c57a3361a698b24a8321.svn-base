$(function () {
    app.viewmodel.isBound(true);
});

function ChangeCaseReviewTypeSection(
    caseReviewTypeId
    , sectionId
    , isIncluded) {

    var data = {
        CaseReviewTypeID: caseReviewTypeId,
        SectionID: sectionId,
        IsIncluded: isIncluded
    };

    app.api.callApi(data, urlApiUpdateCaseReviewTypeSection, true,
        function (callbackData) {
            // success
            app.helpers.showSavedNotification("Updated :-)");
        },
        function (callback) {
            // error
            app.helpers.showErrorNotification("ooops :-(");
        }

    );
}