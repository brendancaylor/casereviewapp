CREATE VIEW [dbo].[vwStaffNonCompliantTotals]
	AS 
	
	SELECT
	newid() as ID
	, SUM(1 - Answer.Compliant) AS SumNonCompliant
	, StaffID
	, Answer.FeedbackType
	, StaffFirstname
	, StaffSurname
	, SectionName
	, SectionID
	, QuestionID
	, Question.IsMandatory

	FROM Answer
	INNER JOIN CaseReviewWorkSheet ON Answer.CaseReviewWorkSheetID = CaseReviewWorkSheet.ID
	INNER JOIN Question ON Answer.QuestionID = Question.ID
	INNER JOIN Section ON Question.SectionID = Section.ID
	INNER JOIN Staff on CaseReviewWorkSheet.StaffID = Staff.ID

	WHERE (Answer.Compliant = 0) AND CaseReviewWorkSheet.ReviewedDate > DATEADD("m",-6, GetDate())
	GROUP BY 
	StaffID
	, Answer.FeedbackType
	, StaffFirstname
	, StaffSurname
	, SectionName
	, SectionID
	, QuestionID
	, Question.IsMandatory
