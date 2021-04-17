using System;
using Meeting_02_04;

namespace Meeting_02_04
{
	public interface ISalary<out T> where T: Money
	{
		T Salary { get; }
	}
	public class Manager : ISalary<Euro>
	{
		public Euro Salary
		{
			get;
			protected set;
		}

		public Manager(double val)
		{
			Salary = new Euro(val);
		}
	}
	public class Programmer : ISalary<Dollar>
	{

		public Dollar Salary { get;protected set; }

		public Programmer(double val)
		{
			Salary = new Dollar(val);
		}
}
	}
	public class Ingineer : ISalary<Rub>
	{ 
		public Rub Salary { get; protected set; }

		public Ingineer(double value)
		{
			Salary = new Rub(value);
		}

	}

	public class SalaryCalculator
	{
		public double Charge { get; protected set; }
		public string Format { get; set; } = "{0:C3}";
		public SalaryCalculator(double charge)
		{
			Charge = charge;
		}

		public void SalaryWithCharge(in ISalary<Money> s)
		{
			s.Salary.Value *= Charge;
			Console.WriteLine($"Salary of {s.GetType()} is {s.Salary}");
		}

	}

