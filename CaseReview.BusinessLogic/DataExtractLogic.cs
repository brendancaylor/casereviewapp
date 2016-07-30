using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;

namespace CaseReview.BusinessLogic
{
    public class DataExtractLogic
    {
        public byte[] DataExtractAll()
        {
            var utils = new Utils();
            var daStaff = new BaseService<Staff>();
            var daCaseReviewType = new BaseService<CaseReviewType>();
            var daSection = new BaseService<Section>();
            var daSectionCaseReviewType = new BaseService<SectionCaseReviewType>();
            var daQuestion = new BaseService<Question>();
            var daCaseReviewWorkSheet = new BaseService<CaseReviewWorkSheet>();
            var daAnswer = new BaseService<Answer>();

            List<FileForZipping> files = new List<FileForZipping>();

            var staffData = utils.ToCsv<Staff>(",", daStaff.GetAll());
            var caseReviewTypeData = utils.ToCsv<CaseReviewType>(",", daCaseReviewType.GetAll());
            var sectionData = utils.ToCsv<Section>(",", daSection.GetAll());
            var sectionCaseReviewTypeData = utils.ToCsv<SectionCaseReviewType>(",", daSectionCaseReviewType.GetAll());
            var questionData = utils.ToCsv<Question>(",", daQuestion.GetAll());
            var caseReviewWorkSheetData = utils.ToCsv<CaseReviewWorkSheet>(",", daCaseReviewWorkSheet.GetAll());
            var answerData = utils.ToCsv<Answer>(",", daAnswer.GetAll());

            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(staffData),
                FileName = "Staffs.csv"
            });
            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(caseReviewTypeData),
                FileName = "CaseReviewTypes.csv"
            });
            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(sectionData),
                FileName = "Sections.csv"
            });
            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(questionData),
                FileName = "Questions.csv"
            });
            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(caseReviewWorkSheetData),
                FileName = "CaseReviewWorkSheets.csv"
            });
            files.Add(new FileForZipping()
            {
                FileData = Encoding.ASCII.GetBytes(answerData),
                FileName = "Answers.csv"
            });

            return utils.CreateZipFile(files);
        }
    }
}
