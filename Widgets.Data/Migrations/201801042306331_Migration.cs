namespace Widgets.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Widget",
                c => new
                    {
                        WidgetId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.WidgetId)
                .ForeignKey("dbo.Cart", t => t.Cart_CartId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true, name: "IX_U_Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cart", "UserId", "dbo.User");
            DropForeignKey("dbo.Widget", "Cart_CartId", "dbo.Cart");
            DropIndex("dbo.User", "IX_U_Email");
            DropIndex("dbo.Widget", new[] { "Cart_CartId" });
            DropIndex("dbo.Cart", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Widget");
            DropTable("dbo.Cart");
        }
    }
}
