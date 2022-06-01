namespace BLL.DTOs
{
    public class AuctionDTO
    {
        public int ID { get; set; }
        public int Bid { get; set; }
        public string Leader { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }

        public LotDTO Lot { get; set; }
    }
}
