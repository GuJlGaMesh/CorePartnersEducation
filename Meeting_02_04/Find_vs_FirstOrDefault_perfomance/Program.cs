using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Find_vs_FirstOrDefault_perfomance
{
	class Program
	{
		static void Main(string[] args)
		{
			var s = new Stopwatch();
			int counter = 0;
			string line;

			// Read the file and display it line by line.
			var l = new List<string>();
			StreamReader file =
				new StreamReader(@"C:\Users\sirix\Desktop\CorePartnersEducation\Meeting_02_04\Find_vs_FirstOrDefault_perfomance\text.txt");
			while ((line = file.ReadLine()) != null)
			{
				l.Add(line);

			}
			//sssssssss
			file.Close();
			s.Start();
			var t = l.Find(x => x == "sssssssss");
			s.Stop();
			Console.WriteLine($"find result: {s.ElapsedMilliseconds}");
			s.Reset();
			s.Start();
			var c = l.First(x => x == "sssssssss");
			s.Stop();
			Console.WriteLine($"first result: {s.ElapsedMilliseconds}");
			s.Reset();
			s.Start();
			var c1 = l.FirstOrDefault(x => x == "sssssssss");
			s.Stop();
			Console.WriteLine($"firstordefault result: {s.ElapsedMilliseconds}");
		}
	}
}
