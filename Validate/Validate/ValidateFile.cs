﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Validate
{
    public static class ValidateFile
    {
        static string elementName;
        static string xpath = string.Empty;

        static XElement element;

        /// <summary>
        /// Print element with xpath
        /// </summary>
        /// <param name="elementToFind"></param>
        /// <param name="ContentOrTemplate"></param>
        /// <param name="required"></param>
        private static void PrintElement(string elementToFind, string ContentOrTemplate = "Content", bool required = false)
        {
            ElementSpecificProperties(elementToFind);
            element = Common.FindElement(elementToFind, ContentOrTemplate);

            if (element != null)
            {
                var contentType = element.Ancestors(ContentOrTemplate).Descendants("ContentType").FirstOrDefault().Value;

                if (contentType != "FixedValue")
                {
                    xpath = element.Ancestors(ContentOrTemplate).Descendants("NodePath").Descendants("Path").FirstOrDefault().Value;
                }
                else
                {
                    xpath = "Fixed Value";
                }
                elementName = element.Value;

                var msg = String.Format("{0,-20}{1,-20}", elementName, xpath);
                Console.WriteLine(msg);
            }
            else
            {
                if (required)
                    Common.ErrorList.Add(string.Format("Required element {0} not found", elementToFind));
            }
        }

        //page navigation should not have autoDetect set as action
        private static void PageNavigationAutoDetect()
        {
            var element = Common.RipFile.Descendants("Template").Descendants("TemplateType")
                .Where(e => e.Value == "PageNavigation").FirstOrDefault();
            if (element != null)
                if (element.Parent.Descendants("Action").FirstOrDefault().Value == "AutoDetect")
                    Common.ErrorList.Add("Next page navigation action set to AutoDetect");

        }

        //check proxies are present or not
        private static void AreProxiesPresent()
        {
            int count = Common.RipFile.Descendants("ProxyAddresses").Descendants("ProxyAddress").Count();
            if (count == 0)
                Common.ErrorList.Add("Proxies not present in this file");
        }

        //Note element Misc-Save Content property should be false
        private static void SaveContentFalse()
        {
            var x = Common.RipFile.Descendants("Name")
                .Where(e => e.Value.Contains("DoNotUse") && e.Parent.Name == "Content")?
                .FirstOrDefault()?.Parent;
            if (x != null)
                if (x?.Descendants("IsSaveContent")?.FirstOrDefault()?.Value == "true")
                    Common.ErrorList.Add("TotalJobs_DoNotUse save content property is true ");
        }
        private static void ProjectAdvancedProperties()
        {

            if (Common.RipFile.Descendants("BrowserOptions").Descendants("DisplayFlash").FirstOrDefault().Value.Trim().ToLower() == "true")
                Common.ErrorList.Add("RunActiveXAndFlash property is true in this file");
            if (Common.RipFile.Descendants("IgnorePageLoadErrorCodes").FirstOrDefault().Value.Trim().ToLower() == "true")
                Common.ErrorList.Add("IgnorePageLoadErrorCodes property is true in this file");
            if (Common.RipFile.Descendants("IsRefreshAfterPageLoad").FirstOrDefault().Value.Trim().ToLower() == "true")
                Common.ErrorList.Add("IsRefreshAfterPageLoad property is true in this file");
            if(Common.RipFile.Descendants("AjaxDelayMilliseconds").
                Where(e=>e.Parent.Name== "Template").
                FirstOrDefault().Value.Trim()!="100")
                Common.ErrorList.Add("project advanced properties - Delay after ajax call milliseconds should be set default to 100");
            if (Common.RipFile.Descendants("DelayAfterCompletedActionMilliseconds").
                Where(e => e.Parent.Name == "Template").
                FirstOrDefault().Value.Trim() != "0")
                Common.ErrorList.Add("project advanced properties - Delay after completed action milliseconds should be set default to 0");
            if (Common.RipFile.Descendants("IsWaitForMainDocumentRedirect").
                Where(e => e.Parent.Name == "Template").
                FirstOrDefault().Value.Trim() != "false")
                Common.ErrorList.Add("project advanced properties - Wait for main document redirect should be set to false");//
            if (Common.RipFile.Descendants("IsWaitForAjaxAfterPageLoad").
                Where(e => e.Parent.Name == "Template").
                FirstOrDefault().Value.Trim() != "false")
                Common.ErrorList.Add("project advanced properties -IsWaitForAjaxAfterPageLoad should be set to false");
            if (Common.RipFile.Descendants("IsRandomPageLoadDelay")?.                
                FirstOrDefault()?.Value?.Trim() == "true")
                Common.ErrorList.Add("project advanced properties - IsRandomPageLoadDelay should be set to false");


            var PageLoadDelayMilliseconds = Common.RipFile.Descendants("PageLoadDelayMilliseconds")?.
                FirstOrDefault()?.Value?.Trim();
            if (PageLoadDelayMilliseconds!=null && PageLoadDelayMilliseconds!="1000")
                Common.ErrorList.Add("project advanced properties - Min page load dealy should be set to 1000");

            var PageLoadDelayMaxMilliseconds = Common.RipFile.Descendants("PageLoadDelayMaxMilliseconds")?.
                FirstOrDefault()?.Value?.Trim();
            if (PageLoadDelayMaxMilliseconds != null && PageLoadDelayMaxMilliseconds != "5000")
                Common.ErrorList.Add("project advanced properties - Max page load dealy should be set to 5000");
        }

        private static void ElementSpecificProperties(string elementToFind)
        {

        }

        public static ArrayList IsValid()
        {
            AreProxiesPresent();
            ProjectAdvancedProperties();
            SaveContentFalse();
            PageNavigationAutoDetect();
            if (Common.Filename.Contains("StartUrls"))
                TestStartFile();
            else if (Common.Filename.Contains("Main"))
                TestMainFile();
            else
                testStandaloneFile();

            return Common.ErrorList;

        }

        private static void TestStartFile()
        {
            //required elements
            PrintElement("TotalJobs", "Content", true);
            PrintElement("JobDetailUrl", "Content", true);

            //optional elements
            PrintElement("Jobs", "Template");
            PrintElement("Next", "Template");
            PrintElement("JobTitle", "Content");
            PrintElement("HospitalJobId", "Content");
            PrintElement("HospitalName", "Content");
            PrintElement("JobCategory", "Content");
            PrintElement("City", "Content");
            PrintElement("State", "Content");
            PrintElement("TimeOfDay", "Content");
            PrintElement("Note", "Content");
        }
        private static void TestMainFile()
        {
            if (Common.RipFile.Descendants("IsExtractText").FirstOrDefault().Value == "false")
                Common.ErrorList.Add("The Main script should ALWAYS have the “Include Start URLs in data output” checkbox checked");

            //required elements
            PrintElement("JobDescription", "Content", true);
            PrintElement("JobDescriptionHTML", "Content", true);
            //check starturl element property 
            //FindElement("JobDetailUrl");
            PrintElement("JobTitle", "Content", true);
        }
        private static void testStandaloneFile()
        {

        }
    }
}