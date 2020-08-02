using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Validate
{
    public static class StartFile
    {
        public static void TestStartFile()
        {
            //required elements
            XElement element = Common.FindElement("TotalJobs_DONOTUSE", Common.ElementType.Content);
            if (element == null)
                ValidateFile.PrintElement("TotalJobs", Common.ElementType.Content, true);
            else
                ValidateFile.PrintElement("TotalJobs_DONOTUSE", Common.ElementType.Content, true);
            ValidateFile.PrintElement("JobDetailUrl", Common.ElementType.Content, true);

            //optional elements
            ValidateFile.PrintElement("Jobs", Common.ElementType.Template);
            ValidateFile.PrintElement("Next", Common.ElementType.Template);
            ValidateFile.PrintElement("JobTitle", Common.ElementType.Content);
            ValidateFile.PrintElement("HospitalJobId", Common.ElementType.Content);
            ValidateFile.PrintElement("HospitalName", Common.ElementType.Content);
            ValidateFile.PrintElement("JobCategory", Common.ElementType.Content);
            ValidateFile.PrintElement("City", Common.ElementType.Content);
            ValidateFile.PrintElement("State", Common.ElementType.Content);
            ValidateFile.PrintElement("TimeOfDay", Common.ElementType.Content);
            ValidateFile.PrintElement("Note", Common.ElementType.Content);
        }
    }
}
