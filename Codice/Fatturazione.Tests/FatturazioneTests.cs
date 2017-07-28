using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using NUnit.Framework;

namespace Fatturazione.Tests
{
	[TestFixture]
	public class FatturazioneTests
	{
		/// <summary>
		/// Metodo eseguito prima di tutti i test, prepara il database
		/// </summary>
		[OneTimeSetUp]
		public void Init()
		{
			using (var db = new FatturazioneContext())
			{
				// se non esiste nessun cliente ne aggiunge uno
				if (!db.Clienti.Any())
				{
					var cliente = new Cliente { Cognome = "Rossi", Nome = "Mario" };
					db.Clienti.Add(cliente);
					db.SaveChanges();
				}
				// se non esiste nessuna fattura ne aggiunge 2 al primo cliente
				if (!db.Fatture.Any())
				{
					var cliente = db.Clienti.First();
					db.Fatture.Add(new Fattura { Cliente = cliente });
					db.Fatture.Add(new Fattura { Cliente = cliente });
					db.SaveChanges();
				}
			}
		}

		[Test]
		public void VerificoIlCountDeiClienti()
		{
			using (var db = new FatturazioneContext())
			{
				var conteggio = db.Clienti.Count();
				Assert.That(conteggio, Is.GreaterThan(0));
			}
		}

		[Test]
		public void VerificoCheCreandoUnClienteEUnaFatturaCollegataLiSalvaEntrambe()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = new Cliente { Nome = "VerificoCheCreandoUnClienteEUnaFatturaCollegataLiSalvaEntrambe" };
				var fattura = new Fattura {Cliente = cliente};
				db.Clienti.Add(cliente);
				db.Fatture.Add(fattura);
				Assert.That(cliente.Id, Is.EqualTo(0));
				db.SaveChanges();
				Assert.That(cliente.Id, Is.GreaterThan(0));
			}
		}

		[Test]
		public void VerificoCheCreandoUnClienteEUnaFatturaSenzaCollegarliLiSalvaEntrambe()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = new Cliente { Nome = "VerificoCheCreandoUnClienteEUnaFatturaSenzaCollegarliLiSalvaEntrambe" };
				var fattura = new Fattura ();
				db.Clienti.Add(cliente);
				db.Fatture.Add(fattura);
				Assert.That(cliente.Id, Is.EqualTo(0));
				db.SaveChanges();
				Assert.That(cliente.Id, Is.GreaterThan(0));
				Assert.That(fattura.ClienteId, Is.EqualTo(cliente.Id));
			}
		}

		[Test]
		public void VerificoCheCreandoDueClientiEUnaFatturaSenzaCollegarliRestituisceUnaEccezione()
		{
			using (var db = new FatturazioneContext())
			{
				db.Clienti.Add(new Cliente { Nome = "Cliente1" });
				db.Clienti.Add(new Cliente { Nome = "Cliente2" });
				db.Fatture.Add(new Fattura());
				Assert.Throws<DbUpdateException>(() => db.SaveChanges());
			}
		}

		[Test]
		public void VerificoCheLIncludeMiCaricaLeFatture()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.Include(e => e.Fatture).FirstOrDefault(e => e.Id == 1);
				Assert.That(cliente, Is.Not.Null);
				Assert.That(cliente.Fatture, Is.Not.Null);
				Assert.That(cliente.Fatture.Count, Is.GreaterThan(0));
			}
		}

		[Test]
		public void VerificoCheChiedendoLaListaFuoriDalContestoGeneriUnaEccezione()
		{
			IEnumerable<Cliente> clienti;
			using (var db = new FatturazioneContext())
			{
				clienti = db.Clienti;
			}
			Assert.Throws<InvalidOperationException>(() => clienti.ToList());
		}

		[Test]
		public void VerificoCheCaricandoLaProprietaLazyFuoriDalContestoGeneriUnaEccezione()
		{
			Cliente cliente;
			using (var db = new FatturazioneContext())
			{
				cliente = db.Clienti.First();
				Assert.That(cliente, Is.Not.Null);
			}
			Assert.Throws<ObjectDisposedException>(() => cliente.Fatture.ToList());
		}

		[Test]
		public void VerificoCheUnaEntitaAppenaCaricataSiaInStatoUnchanged()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.FirstOrDefault();
				var entry = db.Entry(cliente);
				Assert.That(entry.State, Is.EqualTo(EntityState.Unchanged));
			}
		}

		[Test]
		public void VerificoCheUnaEntitaCaricataInUnAltroContestoSiaInStatoDetached()
		{
			Cliente cliente;
			using (var db = new FatturazioneContext())
			{
				cliente = db.Clienti.FirstOrDefault();
			}
			using (var db = new FatturazioneContext())
			{
				var entry = db.Entry(cliente);
				Assert.That(entry.State, Is.EqualTo(EntityState.Detached));
			}
		}

		[Test]
		public void VerificoCheUnaEntitaModificataSianInStatoModified()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.First();
				cliente.Cognome = "Modificato";
				var entry = db.Entry(cliente);
				Assert.That(entry.State, Is.EqualTo(EntityState.Modified));
			}
		}

		[Test]
		public void VerificoCheUnaEntitaModificataInModalitaNoTrackingSiaInStatoUnchanged()
		{
			using (var db = new FatturazioneContext())
			{
				db.Configuration.AutoDetectChangesEnabled = false;
				var cliente = db.Clienti.FirstOrDefault();
				cliente.Cognome = "Modificato";
				var entry = db.Entry(cliente);
				Assert.That(entry.State, Is.EqualTo(EntityState.Unchanged));
			}
		}

		[Test]
		public void VerificoCambiamentoManualeDelloStatoERelativoSalvataggio()
		{
			string cognomeModificato;
			using (var db = new FatturazioneContext())
			{
				db.Configuration.AutoDetectChangesEnabled = false;
				var cliente = db.Clienti.FirstOrDefault();
				cliente.Cognome = cliente.Cognome + ".";
				cognomeModificato = cliente.Cognome;
				var entry = db.Entry(cliente);
				entry.State = EntityState.Modified;
				db.SaveChanges();
			}
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.FirstOrDefault();
				Assert.That(cliente.Cognome, Is.EqualTo(cognomeModificato));
			}
		}

		[Test]
		public void VerificoCheUnaNuovaEntitaHaLoStatoDetachedFincheNonVieneAggiuntaAlDbSet()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = new Cliente { Cognome = "Test", Nome = "Di Prova" };
				Assert.That(db.Entry(cliente).State, Is.EqualTo(EntityState.Detached));
				db.Clienti.Add(cliente);
				Assert.That(db.Entry(cliente).State, Is.EqualTo(EntityState.Added));
				Assert.That(cliente.Id, Is.EqualTo(0));
				db.SaveChanges();
				Assert.That(cliente.Id, Is.GreaterThan(0));
				Assert.That(db.Entry(cliente).State, Is.EqualTo(EntityState.Unchanged));
			}
		}

		[Test]
		public void VerificoCheUnaNuovaEntitaCreataConCreateHaLoStatoDetachedEdEUnProxy()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.Create();
				cliente.Nome = "Test";
				cliente.Cognome = "One";
				Assert.That(cliente.GetType().FullName.Contains("Proxies.Cliente_"), Is.True);
				Assert.That(db.Entry(cliente).State, Is.EqualTo(EntityState.Detached));
			}
		}

		[Test]
		public void VerificoCheUnaNuovaEntitaCreataConCreateEProxyCreationDisableRestituisceLEntitaNormale()
		{
			using (var db = new FatturazioneContext())
			{
				db.Configuration.ProxyCreationEnabled = false;
				var cliente = db.Clienti.Create();
				Assert.That(cliente.GetType().FullName.Contains("Proxies.Cliente_"), Is.False);
			}
		}

		[Test]
		public void VerificoCheLoStessoClienteCaricatiDalDbDueVolteRestisceLoStessoOggettoInMemoria()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente1 = db.Clienti.FirstOrDefault();
				var cliente2 = db.Clienti.FirstOrDefault();
				Assert.That(ReferenceEquals(cliente1, cliente2), Is.True);
			}
		}

		[Test]
		public void VerificoCheEseguendoUnaQueryRawSullaTabellaClientiVengonoRitornatiIClienti()
		{
			using (var db = new FatturazioneContext())
			{
				var clienti = db.Database.SqlQuery<Cliente>("SELECT * FROM Clienti").ToList();
				Assert.That(clienti.Count, Is.GreaterThan(0));
			}
		}

		[Test]
		public void VerificoCheEseguendoUnaQueryRawConUnSottoInsiemeDiCampiVieneGenerataUnaEccezione()
		{
			using (var db = new FatturazioneContext())
			{
				var clienti = db.Database.SqlQuery<Cliente>("SELECT Id FROM Clienti");
				Assert.Throws<EntityCommandExecutionException>(() => clienti.ToList());
			}
		}

		[Test]
		public void VerificoCheSeCercoDiSalvareUnaEntitaModificataDaUnAltroContestoGeneraUnaEccezioneDiConcorrenza()
		{
			using (var db = new FatturazioneContext())
			{
				var cliente = db.Clienti.First();
				cliente.Cognome += ".";
				using (var db2 = new FatturazioneContext())
				{
					var cliente2 = db2.Clienti.First();
					cliente2.Cognome += "!";
					db2.SaveChanges();
				}
				Assert.Throws<DbUpdateConcurrencyException>(() => db.SaveChanges());
			}
		}

		[Test]
		[Ignore("Test di performance")]
		public void VerificaDiInserimentoMassivo()
		{
			using (var db = new FatturazioneContext())
			{
				db.Configuration.ProxyCreationEnabled = false;
				db.Configuration.AutoDetectChangesEnabled = false;
				for (int i = 0; i < 10000; i++)
				{
					var cliente = new Cliente();
					db.Entry(cliente).State = EntityState.Added;
				}
				db.SaveChanges();
			}
		}

	}
}