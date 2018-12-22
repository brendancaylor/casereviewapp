CREATE VIEW [dbo].[vwNonCompliant]
	AS 
	
SELECT
	Answer.ID
	, CaseReviewWorkSheet.ClientRef as ClientRef
	, DATEPART(MM, CaseReviewWorkSheet.ReviewedDate) AS Month
	, DATEPART(yy, CaseReviewWorkSheet.ReviewedDate) AS Year
	
	, Section.SectionName
	, Section.DisplayOrder AS SectionOrder
	
	, Question.ID AS QuestionID
	, Question.QuestionName
	, Question.DisplayOrder AS QuestionOrder
	, Question.IsMandatory
	, Question.Risk

	, Staff.ID AS StaffID
	, Staff.StaffSurname
	
	, Comments
	, Feedback
	, FeedbackType
	, CamConfirmation
	, CASE WHEN Feedback IS NULL OR Feedback = '' THEN cast(0 AS BIT) ELSE cast(1 AS BIT) END AS HasFeedback

	FROM Answer
	INNER JOIN Question ON Answer.QuestionID = Question.ID
	INNER JOIN CaseReviewWorkSheet ON Answer.CaseReviewWorkSheetID = CaseReviewWorkSheet.ID
	INNER JOIN Staff ON CaseReviewWorkSheet.StaffID = Staff.ID
	INNER JOIN Section ON Question.SectionID = Section.ID

	WHERE (Answer.Compliant = 0 or Answer.Advisory = 1)
