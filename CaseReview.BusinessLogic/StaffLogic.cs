using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;

namespace CaseReview.BusinessLogic
{
    public class StaffLogic
    {
        public List<Staff> GetAllStaff()
        {
            var bda = new BaseService<Staff>();
            return bda.GetAll().OrderBy(o => o.StaffSurname).ToList();
        }

        public Staff GetStaff(Guid id)
        {
            var bda = new BaseService<Staff>();
            return bda.Get(id);
        }

        public Staff AddStaff(Staff o)
        {
            var bda = new BaseService<Staff>();
            return bda.Add(o);
        }

        public Staff UpdateStaff(Staff o)
        {
            var bda = new BaseService<Staff>();
            return bda.Update(o);
        }
    }
}
