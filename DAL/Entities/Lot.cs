using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Lot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Details { get; set; }
        public virtual string Owner { get; set; }
        public virtual bool Sold { get; set; }
        public virtual Category Category { get; set; }
    }
}
