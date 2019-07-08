using System.Collections.Generic;
using System.Xml.Linq;

namespace Serko.Core
{
    public interface IExtractor
    {
        
        IEnumerable<XDocument> Extract(string stringContent);
    }
}