$(function () {
    var ld = localData;
    app.viewmodel.CaseReviewTypeSections = ko.mapping.fromJS(ld);
    app.viewmodel.SelectedCaseReviewType = ko.observable();

    app.viewmodel.ChangedIncluded = function (sectionCaseReviewType, evt) {
        ChangeCaseReviewTypeSection(
            app.viewmodel.SelectedCaseReviewType().CaseReviewTypeID()
            , sectionCaseReviewType.SectionID()
            , sectionCaseReviewType.IsIncluded()
            );
    }
});

