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
                    new Fish("Сьомга", FishType.FreshAndSaltWather, "/Images/Fish/salomon.png"),
                    new Fish("Балканска пъстърва", FishType.Freshwather, "/Images/Fish/balkan-trout.jpg"),
                    new Fish("Сьомгова пъстърва", FishType.Freshwather, "/Images/Fish/salomon-trout.png"),
                    new Fish("Шаран", FishType.Freshwather, "/Images/Fish/carp.jpg"),
                    new Fish("Сом", FishType.Freshwather, "/Images/Fish/catfish.jpg"),
                    new Fish("Каракуда", FishType.Freshwather, "/Images/Fish/crucian.jpg")
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
