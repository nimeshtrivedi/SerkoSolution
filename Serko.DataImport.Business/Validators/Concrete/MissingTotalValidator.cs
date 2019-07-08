using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Serko.DataImport.Business.Validators.Abstract;

namespace Serko.DataImport.Business.Validators.Concrete
{
    public class MissingTotalValidator : IElementValidator
    {
      
       public bool Validate(XElement xElement)
       {
           return xElement.DescendantsAndSelf().Any(p =>
               string.Equals(p.Name.LocalName, "total", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(p.Value));
          
       }

       public string Message { get; } = "Missing Total Validator - total is missing or empty";
    }
}
