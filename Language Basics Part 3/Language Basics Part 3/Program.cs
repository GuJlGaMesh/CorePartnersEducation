//#define VERIFY
#define TEST

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Language_Basics_Part_3
{
	class Calculator
	{
		public int Sum(int? a, int? b) => (int)(a.HasValue ? (b.HasValue ? a + b : a) : (b.HasValue ? b : 0));
		public int Dis(int? a, int? b) => (int)(a.HasValue ? (b.HasValue ? a - b : a) : (b.HasValue ? -b : 0));

		public bool BiggerThan(int? a, int? b) =>
			(a.HasValue ? (b.HasValue ? a > b : true) : (b.HasValue ? false : false));


	}
	[Cond]
	class Program
	{
		public delegate void Chain();
		static void Main(string[] args)
		{
			Conditional();
			#region chain
			Chain chain = ChainMember1;
			chain += ChainMember2;
			chain += ChainMember3;
			chain += ChainMember4;
			chain();
			#endregion

			#region chain with catch(подвох)
			var chain2 = new Chain(ChainMember5);
			chain2 += ChainMember6;
			chain2 += ChainMember7;
			var invocArr = chain2.GetInvocationList();
			foreach (var deleg in invocArr)
			{
				try
				{
					deleg.DynamicInvoke();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.InnerException.Message);
					Console.Write(" - it was exception... ");
				}
			}



			#endregion

			#region generic delegate

			var b = new List<int>() {1, 2, 3, 4, 5, 6, 7};
			TransformList(ref b, x=> x^2, x => Console.Beep());
			#endregion

			#region attribute
			Console.WriteLine(Environment.NewLine);
			AttributeDetector();
			#endregion

			#region calculator
			var c = new Calculator();
			Console.WriteLine("calculator:");
			Console.WriteLine("null + null: "+c.Sum(null, null));
			Console.WriteLine("10 + null: "+c.Sum(10,null));
			Console.WriteLine("null + 10:" + c.Sum(null,10));
			Console.WriteLine("10 + 10:" + c.Sum(10,10));

			Console.WriteLine("10-5:"+c.Dis(10,5));
			Console.WriteLine("5-null:"+c.Dis(5,null));
			Console.WriteLine("null - 5:"+c.Dis(null,5));
			Console.WriteLine("null - null: "+c.Dis(null,null));
			
			Console.WriteLine("null > null "+c.BiggerThan(null,null));
			Console.WriteLine("10 > null "+c.BiggerThan(10,null));
			Console.WriteLine("null > 10 "+c.BiggerThan(null,10));
			Console.WriteLine("10 > 9 "+c.BiggerThan(10,9));
			#endregion
			
			
			#region conditional attributes

			#region class of conditional attribute
			#endregion

			
			#endregion
		}

		#region conditional attributes method

		public static void Conditional()
		{
			Console.WriteLine("CondAttribute is {0}applied to Program type.",
				Attribute.IsDefined(typeof(Program),
					typeof(CondAttribute))
					? ""
					: "not ");
		}

		#endregion
		
		#region chain methods
		public static void ChainMember1() => Console.WriteLine("1-st call");
		public static void ChainMember2() => Console.WriteLine("2-st call");
		public static void ChainMember3() => Console.WriteLine("3-st call");
		public static void ChainMember4() => Console.WriteLine("4-st call");
		#endregion

		#region chain methods with exception
		public static void ChainMember5() => Console.WriteLine("It was supposed to be a beautiful day.");

		public static void ChainMember6()
		{
			throw new Exception("Suddenly boom");
		} 
		public static void ChainMember7() => Console.WriteLine("How are we going to live?");
		#endregion

		#region attributes

		

	
		[UInfo("do action on list and transform it")]
		public static void TransformList(ref List<int> b, Func<int,int> func, Action<int> act)
		{
			b.ForEach(act);
			b.ForEach(Console.WriteLine);
			Console.WriteLine(Environment.NewLine);
			b.Select(x => func(x)).ToList().ForEach(Console.WriteLine);
		}
		[UInfo("method tagged by attribute")]
		public static void AttributeDetector()
		{
			Type t = typeof(UInfoAttribute);
			object[] obj = t.GetCustomAttributes(false);
			foreach (object o in obj)
				Console.WriteLine(o);
		}

		[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field )]
		public sealed class UInfoAttribute : Attribute
		{
			public string Desc;
			public UInfoAttribute() { }
			public UInfoAttribute(string str)
			{
				Desc = str;
			}
		}
		#endregion

		#region conditional attributes class
		[Conditional("TEST")][Conditional("VERIFY")]
		public sealed class CondAttribute : Attribute {
		}
		#endregion
	}
}
