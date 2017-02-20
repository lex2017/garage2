namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2.DAL.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Garage2.DAL.GarageContext";
        }

        protected override void Seed(Garage2.DAL.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.VehicleTypes.AddOrUpdate(
                  p => p.VehicleTypeId,
                  new Models.VehicleType { VehicleTypeId = 1 , Type = "Bil" },
                  new Models.VehicleType { VehicleTypeId = 2, Type = "Buss" },
                  new Models.VehicleType { VehicleTypeId = 3, Type = "Båt" },
                  new Models.VehicleType { VehicleTypeId = 4, Type = "Flygplan" },
                  new Models.VehicleType { VehicleTypeId = 5, Type = "Motorcykel" }
                );
        }
    }
}
