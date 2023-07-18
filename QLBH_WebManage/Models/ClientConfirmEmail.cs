namespace QLBH_WebManage.Helper
{
    public class ClientConfirmEmail
    {
        public string userEmail { get; set; }
        public string deliverCode { get; set; }
        public string orderCode { get; set; }
        public bool isCreatedOrder { get; set; } = true;
    }
}
