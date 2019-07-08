using System.Xml.Linq;

namespace Serko.DataImport.Business.Validators.Abstract
{
    public interface IElementValidator
    {
        bool Validate(XElement xElement);
        string Message { get; }
    }
}