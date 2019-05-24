namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSolicitudes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Solicitudes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        DNI = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solicitudes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Solicitudes", new[] { "UserId" });
            DropTable("dbo.Solicitudes");
        }
    }
}
