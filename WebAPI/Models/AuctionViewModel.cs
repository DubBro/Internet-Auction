namespace WebAPI.Models
{
    public class AuctionViewModel
    {
        public int ID { get; set; }
        public int Bid { get; set; }
        public string Leader { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }

        public LotViewModel Lot { get; set; }
    }
}