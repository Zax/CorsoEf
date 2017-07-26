using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corso.Es1
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = new FatturazioneContext();
			Console.WriteLine(db.Clienti.Count());
		}
	}
}
