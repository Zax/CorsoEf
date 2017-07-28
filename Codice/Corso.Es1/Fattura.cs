using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corso.Es1
{
	[Table("Fatture")]
	public class Fattura
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime DataFattura { get; set; } = DateTime.Today;

		public int ClienteId { get; set; }

		[ForeignKey("ClienteId")]
		public Cliente Cliente { get; set; }

	}
}
