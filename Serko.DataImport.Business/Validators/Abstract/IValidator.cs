using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Serko.DataImport.Business.Validators.Abstract
{
    public interface IValidator
    {
        bool Validate(string text);
        string Message { get;  }
    }
}
