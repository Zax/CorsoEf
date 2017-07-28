using System;
using System.Linq;

namespace Fatturazione
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
