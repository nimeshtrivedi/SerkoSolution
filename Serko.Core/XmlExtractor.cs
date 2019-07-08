using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Serko.Core
{
    public class XmlExtractor : IExtractor
    {
        private const string Pattern = @"<(.*)>(.*)</\1>";
        
        public IEnumerable<XDocument> Extract(string stringContent) => Regex.Matches(stringContent, Pattern, RegexOptions.IgnoreCase).Select(a => XDocument.Parse(a.Groups[0].Value));
    }
}