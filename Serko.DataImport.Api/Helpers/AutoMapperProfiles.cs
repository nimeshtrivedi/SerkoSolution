using System.Xml.Linq;
using AutoMapper;
using Serko.DataImport.Api.ViewModels;
using Serko.DataImport.Entities;

namespace Serko.DataImport.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Expense, ExpenseVm>();
            CreateMap<XElement, Expense>()
                .ForMember(m => m.Total, opt => opt.MapFrom(src => GetFloatAttribute(src, "total")))
                .ForMember(m => m.CostCentre, opt => opt.MapFrom(src => GetStringAttribute(src, "cost_centre")))
                .ForMember(m => m.PaymentMethod, opt => opt.MapFrom(src => GetStringAttributeNullIfNull(src, "paymentMethod")));
        }

        private string GetStringAttributeNullIfNull(XElement src, string attributeName)
        {
            return src.Element(attributeName) != null && !string.IsNullOrEmpty(src.Element(attributeName)?.Value) ? src.Element(attributeName)?.Value : string.Empty;
        }

        private string GetStringAttribute(XElement src, string attributeName)
        {
            return src.Element(attributeName) != null && !string.IsNullOrEmpty(src.Element(attributeName)?.Value) ? src.Element(attributeName)?.Value : "UNKNOWN";
        }

        private float GetFloatAttribute(XElement src, string attributeName)
        {
            return src.Element(attributeName) != null && !string.IsNullOrEmpty(src.Element(attributeName)?.Value) ? float.Parse(src.Element(attributeName)?.Value) : 0;
        }
    }
}
