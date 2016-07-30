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
using System.IO;
using System.IO.Compression;


namespace CaseReview.BusinessLogic
{

    public class FileForZipping
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }

    }

    public class Utils
    {

        private bool IsComplex(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(System.Guid)
                || propertyInfo.PropertyType == typeof(System.Int32)
                || propertyInfo.PropertyType == typeof(System.String)
                || propertyInfo.PropertyType == typeof(System.Boolean)
                || propertyInfo.PropertyType == typeof(System.DateTime)
                || propertyInfo.PropertyType == typeof(Nullable<bool>)
                || propertyInfo.PropertyType == typeof(Nullable<Int32>)

                )
            { 
                return false;
            }
            else
            { 
                return true;
            }
        }

        public string ToCsv<T>(string separator, IEnumerable<T> objectlist)
        {            
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder csvBuild = new StringBuilder();
            var FieldNames = new List<string>();
            foreach (var property in properties)
            {
                if (!IsComplex(property))
                {
                    FieldNames.Add("\"" + property.Name.ToString() + "\"");
                }
            }
            csvBuild.Append(HttpUtility.HtmlDecode(string.Join(separator, FieldNames.ToArray())));
            
            foreach (var o in objectlist)
            {
                var values = new List<string>();
                foreach (var property in properties)
                {
                    if (!IsComplex(property))
                    {
                        if(property.GetValue(o) != null)
                        {
                            values.Add("\"" + property.GetValue(o).ToString() + "\"");
                        }
                        else
                        {
                            values.Add("\"\"");
                        }
                    }
                }
                var newLine = System.Environment.NewLine + HttpUtility.HtmlDecode(string.Join(separator, values.ToArray()));
                csvBuild.Append(newLine);
            }

            return csvBuild.ToString();
        }
        

        public byte[] CreateZipFile(
            List<FileForZipping> files
            )
        {
            using (var stream = new MemoryStream())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                        {
                            zipStream.Write(file.FileData, 0, file.FileData.Length);
                        }
                    }
                }
                return stream.ToArray();
            }
        }
        

    }

}
