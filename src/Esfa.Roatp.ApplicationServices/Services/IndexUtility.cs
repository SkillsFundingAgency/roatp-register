using System;
using System.Globalization;

namespace Esfa.Roatp.ApplicationServices.Services
{
    public class IndexUtility
    {
        public static DateTime GetDateFromIndexNameAndDateExtension(string indexName, string aliasName, string dateFormat = "yyyy-MM-dd-HH-mm")
        {
            var date = indexName.Replace(aliasName + "-", "");
            return DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
