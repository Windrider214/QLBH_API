namespace QLBH_WebClient.Models
{
    public class ClientConfirmEmail
    {
        public string userEmail { get; set; }
        public string deliverCode { get; set; }
        public bool isCreatedOrder { get; set; } = true;
    }
}
