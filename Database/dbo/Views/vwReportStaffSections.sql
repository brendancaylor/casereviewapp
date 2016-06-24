CREATE VIEW [dbo].[vwReportStaffSections]
	AS

SELECT
newid() as ID
, DATEPART(MM, CaseReviewWorkSheet.ReviewedDate) AS ReviewedDateMonth
, DATEPART(yy, CaseReviewWorkSheet.ReviewedDate) AS ReviewedDateYear
, (DATEPART(yy, CaseReviewWorkSheet.ReviewedDate) * 100) + (DATEPART(MM, CaseReviewWorkSheet.ReviewedDate)) AS ReviewedDateInt

, SUM(Answer.Compliant + 0) AS SumCompliant
, SUM(1 - Answer.Compliant) AS SumNonCompliant

, Question.SectionID
, Section.SectionName
, CaseReviewWorkSheet.StaffID
, Staff.StaffFirstname
, Staff.StaffSurname

FROM Answer
INNER JOIN Question ON Answer.QuestionID = Question.ID
INNER JOIN Section ON Question.SectionID = Section.ID
INNER JOIN CaseReviewWorkSheet ON Answer.CaseReviewWorkSheetID = CaseReviewWorkSheet.ID
INNER JOIN Staff ON CaseReviewWorkSheet.StaffID = Staff.ID
WHERE (Answer.Compliant IS NOT NULL)
GROUP BY 

DATEPART(MM, CaseReviewWorkSheet.ReviewedDate)
, DATEPART(yy, CaseReviewWorkSheet.ReviewedDate)

, Question.SectionID
, Section.SectionName
, CaseReviewWorkSheet.StaffID
, Staff.StaffFirstname
, Staff.StaffSurname
