using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_02_04
{
	interface ISalary
	{
		int Salary { get; set; }
		double GetSalary();
	}
	class Manager : ISalary
	{
		public int Salary { get; set; }
		public double GetSalary()
		{
			return Salary * 0.87;
		}
	}
	class Programmer : ISalary
	{
		public int Salary { get; set; }
		public double GetSalary()
		{
			return Salary * 0.87;
		}
	}
	class Ingineer : ISalary
	{
		public int Salary { get; set; }
		public double GetSalary()
		{
			return Salary * 0.87;
		}
	}
}
