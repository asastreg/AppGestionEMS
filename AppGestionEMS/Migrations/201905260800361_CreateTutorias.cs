namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTutorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tutorias",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        PracticaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CursoId, t.PracticaId })
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Practicas", t => t.PracticaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.PracticaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tutorias", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorias", "PracticaId", "dbo.Practicas");
            DropForeignKey("dbo.Tutorias", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Tutorias", new[] { "PracticaId" });
            DropIndex("dbo.Tutorias", new[] { "CursoId" });
            DropIndex("dbo.Tutorias", new[] { "UserId" });
            DropTable("dbo.Tutorias");
        }
    }
}
