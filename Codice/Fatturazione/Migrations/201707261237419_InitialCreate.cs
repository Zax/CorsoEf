namespace Fatturazione.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clienti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodiceFiscale = c.String(maxLength: 16),
                        Nome = c.String(maxLength: 1024),
                        Cognome = c.String(maxLength: 1024),
                        DataNascita = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Clienti");
        }
    }
}
