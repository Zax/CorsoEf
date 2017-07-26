using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corso.Es1
{
	public class FatturazioneContext : DbContext
	{
		public FatturazioneContext() : base("TestDb")
		{
			//Database.SetInitializer<FatturazioneContext>(new NullDatabaseInitializer<FatturazioneContext>());
			Database.SetInitializer<FatturazioneContext>(new MigrateDatabaseToLatestVersion<FatturazioneContext,Migrations.Configuration>());
		}

		public DbSet<Cliente> Clienti { get; set; }
	}
}
