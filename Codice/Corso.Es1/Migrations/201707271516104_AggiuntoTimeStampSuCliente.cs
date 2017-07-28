namespace Corso.Es1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntoTimeStampSuCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clienti", "Versione", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clienti", "Versione");
        }
    }
}
