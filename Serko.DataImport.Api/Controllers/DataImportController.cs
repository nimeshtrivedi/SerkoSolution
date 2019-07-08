using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serko.DataImport.Api.ViewModels;
using Serko.DataImport.Business.Services.Abstract;

namespace Serko.DataImport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataImportController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public DataImportController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult PostDataImport([FromBody]InputVm input)
        {
            try
            {
                var expense = _expenseService.Get(input.InputString);
                var expenseToReturn = _mapper.Map<ExpenseVm>(expense);
                return Ok(expenseToReturn);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}