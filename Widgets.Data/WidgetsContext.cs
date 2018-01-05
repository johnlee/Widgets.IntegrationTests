using System.Data.Entity;

namespace Widgets.Data
{
    public class WidgetsContext : DbContext
    {
        public WidgetsContext() : base(ConnectionString) { }

        private static string ConnectionString
        {
            get
            {
                return @"Data Source = (LocalDB)\MSSQLLocalDb; Initial Catalog = Widgets; Integrated Security = True;";
                //return System.Configuration.ConfigurationManager.ConnectionStrings["Widgets"].ConnectionString; 
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new WidgetsMappingConfiguration().ConfigureModelBuilder(modelBuilder);
        }
    }
}
