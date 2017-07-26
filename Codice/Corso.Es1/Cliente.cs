using System;
using System.Collections.Generic;
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
		public string Nome { get; set; }
		public string Cognome { get; set; }
	}
}
