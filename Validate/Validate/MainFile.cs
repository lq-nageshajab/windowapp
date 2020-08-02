using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validate
{
    public static class MainFile
    {
        public static void TestMainFile()
        {
            if (Common.RipFile.Descendants("IsExtractText").FirstOrDefault().Value == "false")
                Common.ErrorList.Add("The Main script should ALWAYS have the “Include Start URLs in data output” checkbox checked");

            //required elements
            ValidateFile.PrintElement("JobDescription",Common.ElementType.Content, true);
            ValidateFile.PrintElement("JobDescriptionHTML", Common.ElementType.Content, true);            
            ValidateFile.PrintElement("JobTitle", Common.ElementType.Content, true);
            ValidateFile.PrintElement("ScrapeTime", Common.ElementType.Content, true);
            ValidateFile.PrintElement("UniqueJobKey", Common.ElementType.Content, true);
            ValidateFile.PrintElement("WholePosting", Common.ElementType.Content, true);
            ValidateFile.PrintElement("WholePostingHTML", Common.ElementType.Content, true);

            //optional elements
            ValidateFile.PrintElement("ApplyUrl", Common.ElementType.Content, false);
            ValidateFile.PrintElement("City", Common.ElementType.Content, true);
            ValidateFile.PrintElement("DatePosted", Common.ElementType.Content, false);
            ValidateFile.PrintElement("Department", Common.ElementType.Content, false);
            ValidateFile.PrintElement("EmployeeType", Common.ElementType.Content, false);
            ValidateFile.PrintElement("EmploymentType", Common.ElementType.Content, false);
            ValidateFile.PrintElement("HospitalJobId", Common.ElementType.Content, false);
            ValidateFile.PrintElement("HospitalName", Common.ElementType.Content, true);
            ValidateFile.PrintElement("JobCategory", Common.ElementType.Content, false);
            ValidateFile.PrintElement("JobCode", Common.ElementType.Content, false);
            ValidateFile.PrintElement("NumberOfVacancies", Common.ElementType.Content, false);
            ValidateFile.PrintElement("Specialty", Common.ElementType.Content, false);
            ValidateFile.PrintElement("State", Common.ElementType.Content, false);
            ValidateFile.PrintElement("TimeOfDay", Common.ElementType.Content, false);
            ValidateFile.PrintElement("TimeOfWeek", Common.ElementType.Content, false);
            ValidateFile.PrintElement("TotalJobs", Common.ElementType.Content, true);
            ValidateFile.PrintElement("ApplyUrlInternal", Common.ElementType.Content, false);
            ValidateFile.PrintElement("BenefitsEligible", Common.ElementType.Content, false);
            ValidateFile.PrintElement("ContactEmailAddress", Common.ElementType.Content, false);
            ValidateFile.PrintElement("ContactFaxNumber", Common.ElementType.Content, false);

            ValidateFile.PrintElement("ContactName", Common.ElementType.Content, false);
            ValidateFile.PrintElement("ContactPhoneNumber", Common.ElementType.Content, false);
            ValidateFile.PrintElement("DateToRemove", Common.ElementType.Content, false);
            ValidateFile.PrintElement("Education", Common.ElementType.Content, false);
            ValidateFile.PrintElement("EmployerType", Common.ElementType.Content, false);
            ValidateFile.PrintElement("EmploymentRelationship", Common.ElementType.Content, false);
            ValidateFile.PrintElement("FeaturedJob", Common.ElementType.Content, false);
            ValidateFile.PrintElement("FLSAStatus", Common.ElementType.Content, false);
            ValidateFile.PrintElement("HourlyPayRate", Common.ElementType.Content, false);

            ValidateFile.PrintElement("HoursPerPayPeriod", Common.ElementType.Content, false);
            ValidateFile.PrintElement("HoursPerWeek", Common.ElementType.Content, false);
            ValidateFile.PrintElement("InternalOnly", Common.ElementType.Content, false);
            ValidateFile.PrintElement("LastUpdated", Common.ElementType.Content, false);
            ValidateFile.PrintElement("LocationName", Common.ElementType.Content, false);
            ValidateFile.PrintElement("RelocationOffered", Common.ElementType.Content, false);
            ValidateFile.PrintElement("Salary", Common.ElementType.Content, false);
            ValidateFile.PrintElement("ShiftDuration", Common.ElementType.Content, false);
            ValidateFile.PrintElement("SigningBonus", Common.ElementType.Content, false);

            ValidateFile.PrintElement("TravelRequired", Common.ElementType.Content, false);
            ValidateFile.PrintElement("YearsOfExperience", Common.ElementType.Content, false);
            ValidateFile.PrintElement("CivilServiceClassification", Common.ElementType.Content, false);
            ValidateFile.PrintElement("SourceOfFunding", Common.ElementType.Content, false);
            ValidateFile.PrintElement("CompanyName", Common.ElementType.Content, false);
            ValidateFile.PrintElement("HealthSystemName", Common.ElementType.Content, false);
            ValidateFile.PrintElement("SubsidiaryName", Common.ElementType.Content, false);
        }
    }
}
