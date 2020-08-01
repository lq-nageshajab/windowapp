using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Validate
{
    public static class Common
    {
        public static XElement RipFile { get; set; }
        public static string Filename { get; set; }
        public static ArrayList ErrorList ;
        public static XElement FindElement(string elementToFind, string ContentOrTemplate = "Content")
        {
            var element = RipFile.Descendants("Name")
                  .Where(e => e.Value == elementToFind && e.Parent.Name == ContentOrTemplate)
                  .FirstOrDefault();

            return element;
        }
    }
}
