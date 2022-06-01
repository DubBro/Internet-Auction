namespace WebAPI.Models
{
    public class LotViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Owner { get; set; }
        public bool Sold { get; set; }
        public string Category { get; set; }
    }
}