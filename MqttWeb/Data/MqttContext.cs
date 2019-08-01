
 using Microsoft.EntityFrameworkCore;

namespace MqttWeb.Data
{
    public class MqttContext : DbContext
    {
        public MqttContext()
        {
        }

        public MqttContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MqttConfiguration> Configurations { get; set; }
        public DbSet<MqttSubscription> Subscriptions { get; set; }

        //public DbSet<Pizza> Pizzas { get; set; }

        //public DbSet<PizzaSpecial> Specials { get; set; }

        //public DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring a many-to-many special -> topping relationship that is friendly for serialisation
            //modelBuilder.Entity<PizzaTopping>().HasKey(pst => new { pst.PizzaId, pst.ToppingId });
            //modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(ps => ps.Toppings);
            //modelBuilder.Entity<PizzaTopping>().HasOne(pst => pst.Topping).WithMany();

            // Inline the Lat-Long pairs in Order rather than having a FK to another table
            //modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
            modelBuilder.Entity<MqttConfiguration>().HasKey(mc => mc.Id);
            modelBuilder.Entity<MqttSubscription>().HasKey(ms => ms.Id);
            //modelBuilder.Entity<MqttSubscription1>().HasOne<MqttConfiguration>().WithMany("Subscriptions");
        }
    }
}

