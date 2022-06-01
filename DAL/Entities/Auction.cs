using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Auction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual int Bid { get; set; }
        public virtual string Leader { get; set; }
        public virtual bool Started { get; set; }
        public virtual bool Ended { get; set; }
    }
}
