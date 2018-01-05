using System.Data.Entity.Migrations;

namespace Widgets.Data.Migrations
{
    public sealed class WidgetsConfiguration : DbMigrationsConfiguration<WidgetsContext>
    {
        public WidgetsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WidgetsContext context)
        {
            context.Widgets.AddOrUpdate(Widget.Create
            (
                description: "Optimus Primer",
                price: 150.80m
            ), Widget.Create
            (
                description: "Bumblebee Knoxbox",
                price: 65.60m
            ), Widget.Create
            (
                description: "Megatronic Sonic",
                price: 425.00m
            ));
        }
    }
}
