using System;
using System.Linq;

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
