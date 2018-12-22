using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CaseReview.DataLayer.Models;

namespace CaseReview.DataLayer
{
    public class GeneralDa
    {
        public CaseReviewWorkSheet GetCaseReviewWorkSheet(Guid id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.CaseReviewWorkSheets
                    .Include(o => o.Staff)
                    .Include(o => o.CaseReviewType)
                    .Include(o => o.Answers.Select(a => a.Question.Section))
                    .FirstOrDefault(o => o.ID == id);
            }
        }

        public List<CaseReviewWorkSheet> SearchCaseReviewWorkSheets(DateTime? date, string surname)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                var qry = db.CaseReviewWorkSheets.Where(o => !string.IsNullOrEmpty(o.ClientRef)).Include(o => o.Staff)
                    .Include(o => o.Answers);

                if (date.HasValue)
                {
                    qry = qry.Where(o => o.ReviewedDate.Month == date.Value.Month && o.ReviewedDate.Year == date.Value.Year);
                }

                if (!string.IsNullOrEmpty(surname))
                {
                    qry = qry.Where(o => o.Staff.StaffSurname.Contains(surname));
                }
                qry = qry.Include(o => o.CaseReviewType);
                qry = qry.OrderByDescending(o => o.ReviewedDate);
                var data = qry.ToList();
                return data;
            }

        }

        public List<CaseReviewWorkSheet> GetAllCaseReviewWorkSheets()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.CaseReviewWorkSheets
                    .Include(o => o.Staff)
                    .Include(o => o.Answers)
                    .OrderByDescending(o => o.ReviewedDate)
                    .ToList();
            }
        }

        public List<Section> GetAllSectionsAndQuestions()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Sections
                    .Include(o => o.Questions)
                    .OrderBy(o => o.DisplayOrder)
                    .ToList();
            }
        }

        public List<Question> GetAllQuestions()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Questions
                    .Include(o => o.Section)
                    .OrderBy(o => o.Section.DisplayOrder)
                    .ThenBy(o => o.DisplayOrder)                    
                    .ToList();
            }
        }

        public List<Question> GetAllQuestionsByType(Guid typeId)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                List<Question> result = new List<Question>();
                var sectionCaseReviewTypes = _context.SectionCaseReviewTypes.Where(o => o.CaseReviewTypeID == typeId).ToList();
                foreach (var item in sectionCaseReviewTypes)
                {
                    result.AddRange(_context.Questions.Where(o => o.SectionID == item.SectionID && o.Section.IsActive && o.IsActive));
                }
                return result;
                /*
                return _context.Questions
                    .Include(o => o.Section)
                    .Include(o => o.Section.SectionCaseReviewTypes)
                    .Where(o => o.Section.SectionCaseReviewTypes.All(s => s.CaseReviewTypeID == typeId))
                    .OrderBy(o => o.Section.DisplayOrder)
                    .ThenBy(o => o.DisplayOrder)
                    .ToList();
                */
            }
        }

        public void UpdateCaseReviewTypeSection(
            Guid CaseReviewTypeID
            , Guid SectionID
            , bool IsIncluded)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                var rec = _context.SectionCaseReviewTypes.FirstOrDefault(
                    o => o.SectionID == SectionID
                    && o.CaseReviewTypeID == CaseReviewTypeID
                    );

                if (IsIncluded)
                {
                    // add if doesn't exist
                    if(rec == null)
                    {
                        _context.SectionCaseReviewTypes.Add(
                            new SectionCaseReviewType()
                            {
                                ID = Guid.Empty,
                                CaseReviewTypeID = CaseReviewTypeID,
                                SectionID = SectionID
                            }
                            );
                    }
                }
                else
                {
                    // remove if exists
                    _context.SectionCaseReviewTypes.Remove(rec);
                }
                _context.SaveChanges();
            }
        }
        public void DeleteCaseReviewWorkSheet(Guid id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                var rec =_context.CaseReviewWorkSheets.FirstOrDefault(o => o.ID == id);
                if(rec.Answers != null)
                {
                    rec.Answers.Clear();
                    _context.Answers.RemoveRange(_context.Answers.Where(o => o.CaseReviewWorkSheetID == id));
                }                    
                _context.CaseReviewWorkSheets.Remove(rec);
                _context.SaveChanges();
            }
        }

        public void UpdateAnswer(Answer answer)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                db.Answers.Attach(answer);
                db.Entry(answer).Property(x => x.Comments).IsModified = true;
                db.Entry(answer).Property(x => x.Compliant).IsModified = true;
                db.Entry(answer).Property(x => x.Advisory).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerFeedback(Answer answer)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                db.Answers.Attach(answer);
                db.Entry(answer).Property(x => x.Feedback).IsModified = true;
                db.Entry(answer).Property(x => x.FeedbackType).IsModified = true;
                db.SaveChanges();
            }
        }

        public List<vwNonCompliant> SearchvwNonCompliant(DateTime? date, string surname)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                var qry = db.vwNonCompliants.Where(o => o.SectionOrder > -99);
                
                if (date.HasValue)
                {
                    qry = qry.Where(o => o.Month == date.Value.Month && o.Year == date.Value.Year);
                }

                if (!string.IsNullOrEmpty(surname))
                {
                    qry = qry.Where(o => o.StaffSurname.Contains(surname));
                }
                //qry = qry.OrderBy(o => o.SectionOrder).ThenBy(o => o.QuestionOrder);
                qry = qry.OrderBy(o => o.Year).ThenBy(o => o.Month).ThenBy(o => o.SectionOrder).ThenBy(o => o.QuestionOrder);
                var data = qry.ToList();
                return data;
            }
        }

        public List<Answer> GetCamsAnswers(string email)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                var qry = db.Answers.Where(o => o.CaseReviewWorkSheet.Staff.Email == email
                && (!o.Compliant.Value || o.Advisory)
                && !o.CamConfirmation.HasValue
                && o.Feedback != null).Include(o => o.Question).Include(o => o.Question.Section);
                qry = qry.OrderBy(o => o.CaseReviewWorkSheet.ReviewedDate);
                var data = qry.ToList();
                return data;
            }
        }

        public void AcceptNonCompliance(string email)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                db.Answers.Where(o => o.CaseReviewWorkSheet.Staff.Email == email
                && (!o.Compliant.Value || o.Advisory)
                && !o.CamConfirmation.HasValue
                && o.Feedback != null).ToList().ForEach(o => o.CamConfirmation = DateTime.Now);
                db.SaveChanges();
            }
        }
    }
}
