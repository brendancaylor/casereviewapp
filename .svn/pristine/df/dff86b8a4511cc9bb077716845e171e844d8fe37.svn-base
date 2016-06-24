using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;
using CaseReview.BusinessObjects;



namespace CaseReview.BusinessLogic
{
    public class CsvUtils
    {

        public IEnumerable<string> ToCsv<T>(string separator, IEnumerable<T> objectlist)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            yield return "\"" + String.Join(separator, properties.Select(f => f.Name).ToArray()) + "\"";
            foreach (var o in objectlist)
            {
                yield return "\"" + HttpUtility.HtmlDecode(string.Join(separator, properties.Select(f => (f.GetValue(o) ?? "").ToString()).ToArray())) + "\"";
            }
        }

        

    }

}
