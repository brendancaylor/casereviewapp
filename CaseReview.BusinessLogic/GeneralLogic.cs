using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;

namespace CaseReview.BusinessLogic
{
    public class GeneralLogic
    {
        public List<Section> GetAllSection()
        {
            var bda = new BaseService<Section>();
            var data = bda.GetAll();
            data.OrderBy(o => o.DisplayOrder);
            return data.ToList();
        }

        public Section GetSection(Guid id)
        {
            var bda = new BaseService<Section>();
            return bda.Get(id);
        }

        public Section AddSection(Section o)
        {
            var bda = new BaseService<Section>();
            return bda.Add(o);
        }

        public Section UpdateSection(Section o)
        {
            var bda = new BaseService<Section>();
            return bda.Update(o);
        }

        public List<Section> GetAllSectionsAndQuestions()
        {
            return new GeneralDa().GetAllSectionsAndQuestions();
        }
        

        public List<Question> GetAllQuestion()
        {
            return new GeneralDa().GetAllQuestions();
        }
        public List<Question> GetAllQuestionByType(Guid typeId)
        {
            return new GeneralDa().GetAllQuestionsByType(typeId);
        }

        
        public Question GetQuestion(Guid id)
        {
            var bda = new BaseService<Question>();
            return bda.Get(id);
        }

        public Question AddQuestion(Question o)
        {
            var bda = new BaseService<Question>();
            return bda.Add(o);
        }

        public Question UpdateQuestion(Question o)
        {
            var bda = new BaseService<Question>();
            return bda.Update(o);
        }

        public List<StandardLine> GetAllStandardLine()
        {
            var bda = new BaseService<StandardLine>();
            var data = bda.GetAll();
            data.OrderBy(o => o.Line);
            return data.ToList();
        }

        public List<StandardLine> GetAllStandardLine(string lineType)
        {
            var bda = new BaseService<StandardLine>();
            var data = bda.FindAll(o => o.LineType == lineType);
            data.OrderBy(o => o.Line);
            return data.ToList();
        }

        public StandardLine GetStandardLine(Guid id)
        {
            var bda = new BaseService<StandardLine>();
            return bda.Get(id);
        }

        public StandardLine AddStandardLine(StandardLine o)
        {
            var bda = new BaseService<StandardLine>();
            return bda.Add(o);
        }

        public StandardLine UpdateStandardLine(StandardLine o)
        {
            var bda = new BaseService<StandardLine>();
            return bda.Update(o);
        }

        public void UpdateAnswerFeedback(Answer answer)
        {
            var da = new BaseService<Answer>();
            var dmAnswer = da.Get(answer.ID);
            dmAnswer.Feedback = answer.Feedback;
            dmAnswer.FeedbackType = answer.FeedbackType;
            new GeneralDa().UpdateAnswerFeedback(dmAnswer);
        }
        
        public void UpdateAnswer(Answer answer)
        {
            new GeneralDa().UpdateAnswer(answer);
        }

        public List<vwNonCompliant> SearchvwNonCompliant(DateTime? date, string surname)
        {
            return new GeneralDa().SearchvwNonCompliant(date, surname);
        }

        public void UpdateCaseReviewTypeSection(
            Guid CaseReviewTypeID
            , Guid SectionID
            , bool IsIncluded)
        {
            new GeneralDa().UpdateCaseReviewTypeSection(
            CaseReviewTypeID
            , SectionID
            , IsIncluded);
        }

        public void UpdateCaseReviewType(CaseReviewType model)
        {
            var bda = new BaseService<CaseReviewType>();
            var dbItem = bda.Get(model.ID);
            dbItem.IsActive = model.IsActive;
            dbItem.TypeName = model.TypeName;
            bda.Update(dbItem);
        }

        public void UpdateSectionActiveName(Section model)
        {
            var bda = new BaseService<Section>();
            var dbItem = bda.Get(model.ID);
            dbItem.IsActive = model.IsActive;
            dbItem.SectionName = model.SectionName;
            bda.Update(dbItem);
        }
    }
}
