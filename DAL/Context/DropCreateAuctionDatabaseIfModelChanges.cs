using DAL.Entities;
using System.Data.Entity;

namespace DAL.Context
{
    internal class DropCreateAuctionDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            Lot iphone1 = new Lot { Name = "IPhone", Category = "electronics", Details = "New", Owner = "Taras", Sold = false, Auction = new Auction() };
            Lot mazda = new Lot { Name = "Mazda", Category = "cars", Details = "Mazda RX-7", Owner = "Bob", Sold = false, Auction = new Auction() };
            Lot iphone2 = new Lot { Name = "IPhone", Category = "electronics", Details = "Used", Owner = "Danila", Sold = false, Auction = new Auction() };
            Lot dekameron = new Lot { Name = "Decameron", Category = "literature", Details = "Giovanni Boccaccio `The Decameron`", Owner = "Andrew", Sold = true, Auction = new Auction { Bid = 50, Leader = "Taras", Started = true, Ended = true } };
            Lot toyota = new Lot { Name = "Toyota", Category = "cars", Details = "Toyota Supra", Owner = "Dan", Sold = false, Auction = new Auction { Bid = 2000, Leader = "Scott", Started = true } };
            Lot jeans = new Lot { Name = "Jeans", Category = "clothes", Details = "New, size: M", Owner = "Sam", Sold = false, Auction = new Auction { Bid = 35, Leader = null, Started = true } };

            Lot[] lots = new Lot[] { iphone1, mazda, iphone2, dekameron, toyota, jeans };

            foreach (var lot in lots)
            {
                context.Lots.Add(lot);
            }

            context.SaveChanges();
        }
    }
}
