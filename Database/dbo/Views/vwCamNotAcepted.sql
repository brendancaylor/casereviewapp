CREATE VIEW [dbo].[vwCamNotAcepted]
	AS 

	SELECT
	COUNT(*) AS CountNotAccepted,
	Staff.Email

	FROM Answer

	INNER JOIN CaseReviewWorkSheet ON Answer.CaseReviewWorkSheetID = CaseReviewWorkSheet.ID
	INNER JOIN Staff ON CaseReviewWorkSheet.StaffID = Staff.ID
	WHERE
	(Answer.Compliant = 0 or Answer.Advisory = 1)
	AND (Answer.CamConfirmation IS NULL)
	AND (Answer.Feedback IS NOT NULL)

	GROUP BY Staff.Email

