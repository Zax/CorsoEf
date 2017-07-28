using System;
using System.Linq;

namespace Corso.Es1
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new FatturazioneContext())
			{
				Console.WriteLine($"Clienti: {db.Clienti.Count()}");
				Console.WriteLine($"Fatture: {db.Fatture.Count()}");
			}
		}
	}
}
