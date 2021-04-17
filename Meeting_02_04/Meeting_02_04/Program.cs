using System;
using System.Collections.Generic;
using System.Linq;

namespace Meeting_02_04
{
	class Program
	{
		static void Main(string[] args)
		{

			#region events
			var nm = new NewMail("my mom", "me", "don't forget 'bout dinner");
			nm.newMail += Handler;
			nm.Start();
			#endregion

			#region list<user>
			var list = new List<User>();
			var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			var rnd = new Random();
			var u = new User("vasya", "vasyev", dt.AddMinutes(rnd.NextDouble()));
			var u1 = new User("petya", "petin", dt.AddMinutes(rnd.NextDouble()));
			var u2 = new User("zhenya", "stvol", dt.AddMinutes(rnd.NextDouble()));
			list.AddRange(new[] { u, u1, u2 });
			var noUser = new User("that person", "doesn't exist", DateTime.Now);
			list.Sort((x, y) => x.CompareTo(y));
			Console.WriteLine(list.Find(x => x.FirstName == "kolya") ?? noUser);
			list.ForEach(Console.WriteLine);

			#endregion

			#region dictionary<user>
			var dict = new Dictionary<int, User>();
			for (int i = 0; i < list.Count; i++)
				dict[i] = list[i];
			Console.WriteLine(dict.FirstOrDefault(x => x.Value.FirstName == "zhenya").Value ?? noUser);
			#endregion

			#region constraints
			Console.WriteLine("linked list");
			Node<int> head = new Node<Int32>(0);
			for (int i = 1; i < 10; i++)
				head = new Node<int>(i, head);
			Console.WriteLine(head);
			#endregion

			#region generic type

			var svo = new SetValueOnce<string>();
			svo.Value = "new value";
			svo.Value = "another value";
			Console.WriteLine(svo.ToString());


			#endregion

			#region salary

			var p = new Programmer(5000);
			var m = new Manager(5000);
			var ing = new Ingineer(5000);
			var calc = new SalaryCalculator(0.87);
			calc.SalaryWithCharge(m);
			calc.SalaryWithCharge(ing);
			calc.SalaryWithCharge(p);
			
			#endregion
		}
		public static void Handler(object sender, NewMailEventArgs e) => Console.WriteLine($"New mail received\nFrom: {e.From};\nTo: {e.To}\nWith text: {e.Text}");

	}
}
