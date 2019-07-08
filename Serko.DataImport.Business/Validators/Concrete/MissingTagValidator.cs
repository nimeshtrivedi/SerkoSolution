using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Serko.DataImport.Business.Validators.Abstract;

namespace Serko.DataImport.Business.Validators.Concrete
{
    public class MissingTagValidator : IValidator
    {
        public const string StartTagRegx = "<(\\w+)>";


        public const string CloseTagRegex = "</(\\w+)>";

        
        public bool Validate(string text)
        {
            var startTagCollection = Regex.Matches(text, StartTagRegx).OrderBy(a => a.Value).ToList();
            var endTagCollection = Regex.Matches(text, CloseTagRegex).OrderBy(a => a.Value).ToList();
            //if (startTagCollection.Count != endTagCollection.Count) return false;
            for (var i = 0; i < startTagCollection.Count; i++)
            {
                if (String.Equals(startTagCollection[i].Value.Insert(1, "/"), endTagCollection[i].Value)) continue;
                Message += $"{startTagCollection[i].Value} does not have closing tag";
                return false;
            }
            return true;
        }

        public string Message { get; private set; } = "Missing Tag Validator: ";
    }

    
}
