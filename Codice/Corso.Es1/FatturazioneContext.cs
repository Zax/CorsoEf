﻿using System.Data.Entity;

namespace Corso.Es1
{
	public class FatturazioneContext : DbContext
	{
		public FatturazioneContext() : this("FatturazioneDb") { }
		public FatturazioneContext(string nomeConnectionString) : base(nomeConnectionString) // specifico il nome della connessione da usare
		{
			//Database.SetInitializer<FatturazioneContext>(new NullDatabaseInitializer<FatturazioneContext>());
			//Database.SetInitializer<FatturazioneContext>(new MigrateDatabaseToLatestVersion<FatturazioneContext,Migrations.Configuration>());
		}

		public IDbSet<Cliente> Clienti { get; set; }

		public IDbSet<Fattura> Fatture { get; set; }

	}
}
