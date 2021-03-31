using System;
using System.Collections.Generic;
using System.Linq;
using Meeting_26_03.Indexators;

namespace Meeting_26_03
{
	class Program
	{
		static void Main(string[] args)
		{
			#region params keyword
			Method(1, 2, 3, 4, 5, 6, 7, 8);
			Method(new int[] { 1, 2, 3, 4, 5 });
			#endregion

			#region Virtual problem

			try
			{
				Console.WriteLine("создание виртуального метода");
				var c = new Child();
				c.LogState();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			#endregion

			#region Порядок вызова конструкторов
			var w = new WithStaticField();
			var w1 = new WithStaticField();
			var w2 = new WithStaticField();
			var v = new WithoutStaticField();
			var v1 = new WithoutStaticField();
			var v2 = new WithoutStaticField();
			#endregion

			#region Indexators
			var cars = new List<Car>()
			{
				new Car() {Name = "Geely", Number = "280OC"},
				new Car() {Name = "BMW", Number = "777YY"}
			};
			var p = new Parking();
			foreach (var car in cars)
				p.Add(car);
			Console.WriteLine($"{cars[0]} parking position: {p[cars[0]]}");
			Console.WriteLine(p[new Car() {Name = "Lada", Number = "123BA"}]);
			Console.WriteLine(p["280OC"]);
			Console.WriteLine(p["281OC"]);

			#endregion
		}
		// 3
		static void Method(params int[] a)
		{
			Console.WriteLine(a.GetType());
			a.ToList().ForEach(x => Console.Write($"{x} "));
			Console.WriteLine();
		}


	}


}
