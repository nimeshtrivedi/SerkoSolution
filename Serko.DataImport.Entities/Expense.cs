using System;
using System.Collections.Generic;
using System.Text;

namespace Serko.DataImport.Entities
{
    public class Expense
    {
        public float Total { get; set; }
        public string CostCentre { get; set; } = "Unknown";
        public string PaymentMethod { get; set; }

    }
}
