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
        public static ArrayList ErrorList;

        public enum ElementType
        {
            Content = 0,
            Template = 1
        }
        public static XElement FindElement(string elementToFind, ElementType elementType=ElementType.Content)
        {
            var element = RipFile.Descendants("Name")
                  .Where(e => e.Value == elementToFind && e.Parent.Name == elementType.ToString())
                  .FirstOrDefault();

            return element;
        }
    }
}
