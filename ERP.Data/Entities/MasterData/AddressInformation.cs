namespace ERP.Data.Entities.MasterData
{
    public class AddressInformation:Base
    {
        public string addressFor { get; set; }
        public int? masterId { get; set; }
        public string addressType { get; set; }
        public string houseNo { get; set; }
        public string roadNo { get; set; }
        public string area { get; set; }
        public string city { get; set; }
    }
}
