using System;
using System.Linq;
using System.Xml.Linq;
using AutoMapper;
using Serko.Core;
using Serko.DataImport.Business.Services.Abstract;
using Serko.DataImport.Business.Validators.Abstract;
using Serko.DataImport.Entities;

namespace Serko.DataImport.Business.Services.Concrete
{
    public class ExpenseService :IExpenseService
    {
        private readonly IExtractor _extractor;
        private readonly IValidator _validators;
        private readonly IElementValidator _missingTotalValidator;
        private readonly IMapper _mapper;

        public ExpenseService(IExtractor extractor, IValidator validators, IElementValidator missingTotalValidator, IMapper mapper)
        {
            _extractor = extractor;
            _validators = validators;
            _missingTotalValidator = missingTotalValidator;
            _mapper = mapper;
        }
        public Expense Get(string text)
        {

            if(!_validators.Validate(text)) throw new Exception(_validators.Message);

            
            var xDocCollection = _extractor.Extract(text).ToList();
            
          

            var expenseXml = xDocCollection.Descendants().FirstOrDefault(p => string.Equals( p.Name.LocalName, "Expense", StringComparison.OrdinalIgnoreCase));
            if (xDocCollection.Count == 0) throw new Exception("No XML found");
            if (expenseXml == null) throw new Exception("No expenses found");
            if (!_missingTotalValidator.Validate(expenseXml)) throw new Exception(_missingTotalValidator.Message);

            return _mapper.Map<Expense>(expenseXml);
          
            
            
        }
    }
}
