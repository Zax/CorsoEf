namespace Fatturazione.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntaTabellaFatture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fatture",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataFattura = c.DateTime(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clienti", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fatture", "ClienteId", "dbo.Clienti");
            DropIndex("dbo.Fatture", new[] { "ClienteId" });
            DropTable("dbo.Fatture");
        }
    }
}
