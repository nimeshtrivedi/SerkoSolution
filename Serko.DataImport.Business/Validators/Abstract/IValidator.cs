namespace Serko.DataImport.Business.Validators.Abstract
{
    public interface IValidator
    {
        bool Validate(string text);
        string Message { get;  }
    }
}
