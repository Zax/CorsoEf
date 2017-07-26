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
		public DbSet<Cliente> Clienti { get; set; }
	}
}
