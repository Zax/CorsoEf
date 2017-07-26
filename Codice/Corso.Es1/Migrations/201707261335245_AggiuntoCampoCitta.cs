namespace Corso.Es1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntoCampoCitta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clienti", "Citta", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clienti", "Citta");
        }
    }
}
