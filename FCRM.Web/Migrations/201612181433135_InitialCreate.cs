namespace FCRM.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId, name: "IX_Owner_Id");
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Name = c.String(maxLength: 100),
                        Surname = c.String(maxLength: 100),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId, name: "IX_Company_Id");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId, name: "IX_User_Id");
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        Information = c.String(nullable: false, maxLength: 1024),
                        Price = c.Double(),
                        CompanyId = c.Guid(nullable: false),
                        ManagerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .Index(t => t.CompanyId, name: "IX_Company_Id")
                .Index(t => t.ManagerId, name: "IX_Manager_Id");
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderId = c.Guid(nullable: false),
                        PreformerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PreformerId)
                .Index(t => t.OrderId, name: "IX_Order_Id")
                .Index(t => t.PreformerId, name: "IX_Preformer_Id");
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        SpecificationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId, name: "IX_Specification_Id");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Works", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "PreformerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Specifications", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Works", "IX_Specification_Id");
            DropIndex("dbo.Specifications", "IX_Preformer_Id");
            DropIndex("dbo.Specifications", "IX_Order_Id");
            DropIndex("dbo.Orders", "IX_Manager_Id");
            DropIndex("dbo.Orders", "IX_Company_Id");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", "IX_User_Id");
            DropIndex("dbo.AspNetUsers", "IX_Company_Id");
            DropIndex("dbo.Companies", "IX_Owner_Id");
            DropTable("dbo.Works");
            DropTable("dbo.Specifications");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Companies");
        }
    }
}
