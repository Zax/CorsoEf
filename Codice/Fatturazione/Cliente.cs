using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fatturazione
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

		public virtual List<Fattura> Fatture { get; set; }

		[Timestamp]
		public byte[] Versione { get; set; }

	}
}
