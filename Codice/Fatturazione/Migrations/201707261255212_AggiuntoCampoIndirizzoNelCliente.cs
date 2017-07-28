namespace Fatturazione.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntoCampoIndirizzoNelCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clienti", "Indirizzo", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clienti", "Indirizzo");
        }
    }
}
