using Meeting_26_03.Indexators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Meeting_26_03
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
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

			var geely = new Car() {Name = "Geely", Number = "280OC"};
			var bmw = new Car() {Name = "BMW", Number = "777YY"};

			var cars = new List<Car>()
			{
				geely, bmw
			};
			var p = new Parking();
			foreach (var car in cars)
				p.Add(car);
			Console.WriteLine($"{geely} parking position: {p[geely]}");
			Console.WriteLine(p[new Car() { Name = "Lada", Number = "123BA" }]);
			Console.WriteLine(p["280OC"]);
			Console.WriteLine(p["281OC"]);
			p[bmw] = 5;
			Console.WriteLine(p[5]);
			#endregion
			
			#region ref out
			// я здесь менял out/ref и заметил, что при использовании out нельзя использовать += для объекта, так как он может быть неинициализорован
			// для значимых типов можно инициализоровать переменную, но после метода где параметр обозначен out он всё равно переприсвоится, так что это не особо полезное занятие
			// 
			var i = 0;
			string s = "s";
			ChangeValues(out i,ref s);
			Console.WriteLine($"int: {i} - str: {s}");
			#endregion
			//*/

		}
		// 3
		static void Method(params int[] a)
		{
			Console.WriteLine(a.GetType());
			a.ToList().ForEach(x => Console.Write($"{x} "));
			Console.WriteLine();
		}
		// я здесь менял out/ref и заметил, что при использовании out нельзя использовать += для объекта, так как он может быть неинициализорован
		// для значимых типов можно инициализоровать переменную, но после метода где параметр обозначен out он всё равно переприсвоится, так что это не особо полезное занятие
		// 
		static void ChangeValues(out int i,ref string s)
		{
			i = 10;
			s += "s";
		}

		struct MyStruct
		{
			public int a;
			public int b;
			// ну нельзя так нельзя ¯\_(ツ)_/¯
			public MyStruct()
			{
				a = 5; b = 5;
			}

			public MyStruct(int a=5, int b=5)
			{
				this.a = a;
				this.b = b;
			}
		}
	}


}
