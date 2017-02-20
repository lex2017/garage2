using System.Data.Entity;

namespace Garage2.DAL
{
    public class GarageContext : DbContext
    {    
        public GarageContext() : base("name=GarageContext") { }

      
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.ReceiptViewModel> ReceiptViewModels { get; set; }
        public DbSet<Models.StatisticsViewModel> StatisticsViewModels { get; set; }
        public DbSet<Models.Member> Members { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }
    }
}
