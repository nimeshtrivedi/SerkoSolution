using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using Serko.DataImport.Entities;

namespace Serko.DataImport.Business.Services.Concrete
{

    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<XElement, Expense>()
                .ForMember(m => m.Total, opt=> opt.MapFrom(src => GetFloatAttribute(src, "total" )))
                .ForMember(m=>m.CostCentre, opt => opt.MapFrom(src => GetStringAttribute(src, "cost_centre")))
                .ForMember(m => m.PaymentMethod, opt => opt.MapFrom(src => GetStringAttributeNullIfNull(src, "paymentMethod")));
        }

        private string GetStringAttributeNullIfNull(XElement src, string attributeName)
        {
            return src.Attribute(attributeName) != null ? src.Attribute(attributeName)?.Value : string.Empty;
        }

        private string GetStringAttribute(XElement src, string attributeName)
        {
            return src.Attribute(attributeName) != null ? src.Attribute(attributeName)?.Value : "UNKNOWN";
        }

        private float GetFloatAttribute(XElement src, string attributeName)
        {
            return src.Attribute(attributeName) != null ? float.Parse(src.Attribute(attributeName)?.Value) : 0;
        }
    }
}
