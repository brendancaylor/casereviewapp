using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;

namespace CaseReview.BusinessLogic
{
    public class CaseReviewWorkSheetLogic
    {
        public List<CaseReviewWorkSheet> GetAll()
        {
            return new GeneralDa().GetAllCaseReviewWorkSheets();
        }

        public List<CaseReviewWorkSheet> SearchCaseReviewWorkSheets(DateTime? date, string surname)
        {
            return new GeneralDa().SearchCaseReviewWorkSheets(date, surname);
        }

        public CaseReviewWorkSheet Get(Guid id)
        {
            return new GeneralDa().GetCaseReviewWorkSheet(id);
        }

        public CaseReviewWorkSheet Add(CaseReviewWorkSheet o)
        {
            var bda = new BaseService<CaseReviewWorkSheet>();
            return bda.Add(o);
        }

        public CaseReviewWorkSheet Update(CaseReviewWorkSheet o)
        {
            var bda = new BaseService<CaseReviewWorkSheet>();
            return bda.Update(o);
        }

        public void DeleteCaseReviewWorkSheet(Guid id)
        {
            new GeneralDa().DeleteCaseReviewWorkSheet(id);
        }
    }
}
