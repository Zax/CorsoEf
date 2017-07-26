using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corso.Es1
{
	[Table("Clienti")]
	public class Cliente
	{
		public int Id { get; set; }

		[MaxLength(16)]
		public string CodiceFiscale { get; set; }

		[MaxLength(1024)]
		public string Nome { get; set; }

		[MaxLength(1024)]
		public string Cognome { get; set; }

		public DateTime? DataNascita { get; set; }

		[MaxLength(2000)]
		public string Indirizzo { get; set; }

		[MaxLength(2000)]
		public string Citta { get; set; }

	}
}
