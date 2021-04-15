using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_02_04
{
	interface ISalary
	{
		double GetSalary();
	}
	class Manager : ISalary
	{
		public int Salary { get; set; }
		public double GetSalary()
		{
			return Salary;
		}
	}
	class Programmer : ISalary
	{
		private int _salary;
		
		public double GetSalary()
		{
			return _salary;
		}
		public void SetSalary(int salary)
		{
			_salary = salary;
		}
	}
	class Ingineer : ISalary
	{
		public Ingineer(int salary)
		{
			_salary = salary;
		}

		private int _salary;

		public double GetSalary()
		{
			return _salary;
		}
	}

	class SalaryCalculator
	{
		public double Charge { get; private set; }
		public string Format { get; set; } = "{0:C3}";
		public SalaryCalculator(double charge)
		{
			Charge = charge;
		}
		public double SumWithCharge (ISalary s1, ISalary s2)
		{
			return Charge * (s1.GetSalary() + s2.GetSalary());
		}
		public double SumWithoutCharge (ISalary s1, ISalary s2)
		{
			return (s1.GetSalary() + s2.GetSalary());
		}

		public string GetFormattedSalary(ISalary s)
		{
			if (Format != null)
			{
				return String.Format(Format, s.GetSalary());
			}

			return s.GetSalary().ToString();
		}
	}
}
