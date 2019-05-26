namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePracticas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Practicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Practicas");
        }
    }
}
