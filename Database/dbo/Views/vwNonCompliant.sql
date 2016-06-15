﻿CREATE VIEW [dbo].[vwNonCompliant]
	AS 
	
SELECT
	NEWID() as ID
	, CaseReviewWorkSheet.ClientRef as ClientRef
	, DATEPART(MM, CaseReviewWorkSheet.ReviewedDate) AS Month
	, DATEPART(yy, CaseReviewWorkSheet.ReviewedDate) AS Year
	, Section.SectionName
	, Question.QuestionName
	, Staff.StaffSurname
	, Section.DisplayOrder AS SectionOrder
	, Question.DisplayOrder AS QuestionOrder
	, Comments
	
	FROM Answer
	INNER JOIN Question ON Answer.QuestionID = Question.ID
	INNER JOIN CaseReviewWorkSheet ON Answer.CaseReviewWorkSheetID = CaseReviewWorkSheet.ID
	INNER JOIN Staff ON CaseReviewWorkSheet.StaffID = Staff.ID
	INNER JOIN Section ON Question.SectionID = Section.ID

	WHERE (Answer.Compliant = 0)
