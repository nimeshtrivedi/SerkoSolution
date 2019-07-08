namespace Serko.DataImport.Api.ViewModels
{
    public class ExpenseVm
    {
        public  float Total { get; set; }
        public string CostCentre { get; set; } = "Unknown";
        public string PaymentMethod { get; set; }

        public float Gst { get; set; } = 13.3f;
        
        public float  TotalWithoutGst => Total - (( Total * Gst) / 100);
    }
}
