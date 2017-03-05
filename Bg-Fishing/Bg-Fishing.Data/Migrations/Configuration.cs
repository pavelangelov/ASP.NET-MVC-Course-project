namespace Bg_Fishing.Data.Migrations
{
    using Models;
    using Models.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FishingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FishingContext context)
        {
            if (!context.Fish.Any())
            {
                var fishesArr = new Fish[]
                {
                    new Fish("Сьомга", FishType.FreshAndSaltWather) { ImageUrl = "/Images/Fish/salomon.png"},
                    new Fish("Балканска пъстърва", FishType.Freshwather) { ImageUrl = "/Images/Fish/balkan-trout.jpg" },
                    new Fish("Сьомгова пъстърва", FishType.Freshwather) { ImageUrl = "/Images/Fish/salomon-trout.png" },
                    new Fish("Шаран", FishType.Freshwather) { ImageUrl = "/Images/Fish/carp.jpg" },
                    new Fish("Сом", FishType.Freshwather) { ImageUrl = "/Images/Fish/catfish.jpg" },
                    new Fish("Каракуда", FishType.Freshwather) { ImageUrl = "/Images/Fish/crucian.jpg" }
                };

                foreach (var fish in fishesArr)
                {
                    context.Fish.Add(fish);
                }

                context.SaveChanges();
            }
        }
    }
}
