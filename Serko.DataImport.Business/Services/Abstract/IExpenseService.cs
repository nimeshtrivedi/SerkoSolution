using System.Collections.Generic;
using Serko.DataImport.Entities;

namespace Serko.DataImport.Business.Services.Abstract
{
    public interface IExpenseService
    {
        Expense Get(string text);
    }
}
