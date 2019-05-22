namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEvaluaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluaciones",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                        Tipo_Evalu = c.Int(nullable: false),
                        Nota_Pr = c.Single(nullable: false),
                        Nota_Ev_C = c.Single(nullable: false),
                        Nota_P1 = c.Single(nullable: false),
                        Nota_P2 = c.Single(nullable: false),
                        Nota_P3 = c.Single(nullable: false),
                        Nota_P4 = c.Single(nullable: false),
                        Nota_Final = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId, t.CursoId, t.GrupoId })
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluaciones", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluaciones", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.Evaluaciones", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Evaluaciones", new[] { "GrupoId" });
            DropIndex("dbo.Evaluaciones", new[] { "CursoId" });
            DropIndex("dbo.Evaluaciones", new[] { "UserId" });
            DropTable("dbo.Evaluaciones");
        }
    }
}
