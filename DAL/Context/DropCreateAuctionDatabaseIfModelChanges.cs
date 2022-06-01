using DAL.Entities;
using System.Data.Entity;

namespace DAL.Context
{
    internal class DropCreateAuctionDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            Category clothes = new Category { Name = "Clothes" };
            Category literature = new Category { Name = "Literature" };
            Category cars = new Category { Name = "Cars" };
            Category electronics = new Category { Name = "Electronics" };
            Category sports = new Category { Name = "Sports" };

            Category[] categories = new Category[] { clothes, literature, cars, electronics, sports };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            Lot iphone1 = new Lot { Name = "IPhone", Category = electronics, Details = "New", Owner = "Taras", Sold = false };
            Lot mazda = new Lot { Name = "Mazda", Category = cars, Details = "Mazda RX-7", Owner = "Bob", Sold = false };
            Lot iphone2 = new Lot { Name = "IPhone", Category = electronics, Details = "Used", Owner = "Danila", Sold = false };
            Lot dekameron = new Lot { Name = "Decameron", Category = literature, Details = "Giovanni Boccaccio `The Decameron`", Owner = "Andrew", Sold = true };
            Lot toyota = new Lot { Name = "Toyota", Category = cars, Details = "Toyota Supra", Owner = "Dan", Sold = false };
            Lot ball = new Lot { Name = "Volleyball ball", Category = sports, Details = "Mikasa, used", Owner = "Kate", Sold = false };
            Lot jeans = new Lot { Name = "Jeans", Category = clothes, Details = "New, size: M", Owner = "Sam", Sold = false };
            Lot bike = new Lot { Name = "Bike", Category = sports, Details = "Mountain bike", Owner = "John", Sold = false };

            Lot[] lots = new Lot[] { iphone1, mazda, iphone2, dekameron, toyota, ball, jeans, bike };

            foreach (var lot in lots)
            {
                context.Lots.Add(lot);
            }

            Auction dekameronAuct = new Auction { Lot = dekameron, Bid = 50, Leader = "Taras", Started = true, Ended = true};
            Auction toyotaAuct = new Auction { Lot = toyota, Bid = 2000, Leader = "Scott", Started = true, Ended = false};
            Auction ballAuct = new Auction { Lot = ball, Bid = 60, Leader = "Ann", Started = true, Ended = false};
            Auction jeansAuct = new Auction { Lot = jeans, Bid = 35, Leader = null, Started = true, Ended = false};
            Auction bikeAuct = new Auction { Lot = bike, Bid = 555, Leader = "Sara", Started = true, Ended = false};

            Auction[] auctions = new Auction[] { dekameronAuct, toyotaAuct, ballAuct, jeansAuct, bikeAuct };

            foreach (var auction in auctions)
            {
                context.Auctions.Add(auction);
            }

            context.SaveChanges();
        }
    }
}
