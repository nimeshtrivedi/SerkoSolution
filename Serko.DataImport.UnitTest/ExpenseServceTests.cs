using System;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Serko.Core;
using Serko.DataImport.Api.Helpers;
using Serko.DataImport.Business.Services.Abstract;
using Serko.DataImport.Business.Services.Concrete;
using Serko.DataImport.Business.Validators.Abstract;
using Serko.DataImport.Business.Validators.Concrete;

namespace Serko.DataImport.UnitTest
{
    [TestFixture]
    public class ExpenseServceTests
    {
        private  IExpenseService _expenseService;
        private IValidator _missingTagValidator;
        private IElementValidator _missingTotalValidator;
        private IExtractor _extractor;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Init()
        {
            _missingTotalValidator = new MissingTotalValidator();
            _missingTagValidator = new MissingTagValidator();
            _extractor = new XmlExtractor();
            _mockMapper = new Mock<IMapper>();
            
            var myProfile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);
            _expenseService = new ExpenseService(_extractor, _missingTagValidator, _missingTotalValidator, mapper);
        }

        [Test]
        public void Get_WithoutValidString_ShouldThrowException()
        {
            //arrange
            var textToSend = "";
            //act
           
            //assert
          
            Exception ex = Assert.Throws<Exception>(() => _expenseService.Get(textToSend));
            Assert.That(ex.Message, Is.EqualTo("No XML found"));
        }
        [Test]
        public void Get_WithInvalidEndTag_ShouldThrowException()
        {
            //arrange
            var textToSend = "<expense><cost_centre/> <total>345 </total><payment_method> </expense>";
            //act

            //assert

            Exception ex = Assert.Throws<Exception>(() => _expenseService.Get(textToSend));
            Assert.That(ex.Message, Is.EqualTo("Missing Tag Validator: <payment_method> does not have closing tag"));
        }

        [Test]
        public void Get_WithNoTotal_ShouldThrowException()
        {
            //arrange
            var textToSend = "<expense><cost_centre/>  </expense>";
            //act

            //assert

            Exception ex = Assert.Throws<Exception>(() => _expenseService.Get(textToSend));
            Assert.That(ex.Message, Is.EqualTo("Missing Total Validator - total is missing or empty"));
        }

        [Test]
        public void Get_WithEmptyTotal_ShouldThrowException()
        {
            //arrange
            var textToSend = "<expense><cost_centre></cost_centre> <total> </total> </expense>";
            //act

            //assert

            Exception ex = Assert.Throws<Exception>(() => _expenseService.Get(textToSend));
            Assert.That(ex.Message, Is.EqualTo("Missing Total Validator - total is missing or empty"));
        }

        [Test]
        public void Get_ProperXml_ShouldReturnValidValues()
        {
            //arrange
            var textToSend = "<expense><cost_centre>DEV002</cost_centre> <total>1024.01</total><payment_method>personal card</payment_method> </expense>";
            //act
            var returnValue = _expenseService.Get(textToSend);
            //assert

            Assert.That(returnValue.CostCentre, Is.EqualTo("DEV002"));
        }

        [Test]
        public void Get_XmlWithoutCostCentre_ShouldReturnDefaultValueForCostCentre()
        {
            //arrange
            var textToSend = "<expense><total>1024.01</total><payment_method>personal card</payment_method> </expense>";
            //act
            var returnValue = _expenseService.Get(textToSend);
            //assert

            Assert.That(returnValue.CostCentre, Is.EqualTo("UNKNOWN"));
        }
    }
}
