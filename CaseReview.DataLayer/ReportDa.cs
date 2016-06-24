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
    public class ReportDa
    {

        public List<vwReportStaffSection> SearchVwReportStaffSections(
            int intDateFrom
            , int intDateTo
            , List<Guid> staffIds
            , List<Guid> sectionsIds)
        {
            using (CaseReviewsContext db = new CaseReviewsContext())
            {
                var qry = db.vwReportStaffSections.Where(o => o.ID != Guid.Empty);

                if (staffIds.Any())
                {
                    qry = qry.Where(o => staffIds.Contains(o.StaffID));
                }

                if (sectionsIds.Any())
                {
                    qry = qry.Where(o => sectionsIds.Contains(o.SectionID));
                }
                qry = qry.Where(o => o.ReviewedDateInt >= intDateFrom);
                qry = qry.Where(o => o.ReviewedDateInt <= intDateTo);
                var data = qry.ToList();
                return data;
            }
        }

    }
}
