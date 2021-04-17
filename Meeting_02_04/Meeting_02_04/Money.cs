using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Meeting_02_04
	{

		public abstract class Money
		{
			public double Value;
			public string Currency;
			public Dictionary<string, double> Course;
			public static bool operator <(Money m1, Money m2)
			{
				if (m1.GetType() == m2.GetType())
					return m1.Value < m2.Value;
				return m1.Value * m1.Course[m2.Currency] < m2.Value;
			}
			public static bool operator >(Money m1, Money m2)
			{
				if (m1.GetType() == m2.GetType())
					return m1.Value > m2.Value;
				return m1.Value * m1.Course[m2.Currency] > m2.Value;
			}
			public static UnconvertableMoney operator +(Money m1, Money m2)
			{
				if (m1.GetType() == m2.GetType())
					return new UnconvertableMoney(m1.Value + m2.Value, m2.Currency);
				return new UnconvertableMoney(m1.Value * m1.Course[m2.Currency] + m2.Value, m2.Currency);
			}
			public static UnconvertableMoney operator -(Money m1, Money m2)
			{
				if (m1.GetType() == m2.GetType())
					return new UnconvertableMoney(m1.Value - m2.Value, m2.Currency);
				return new UnconvertableMoney(m1.Value * m1.Course[m2.Currency] - m2.Value, m2.Currency);
			}

			public override string ToString()
			{
				return $"{Value}{Currency}";
			}
		}


		public class Rub : Money
		{
			public Rub(double val)
			{
				Currency = "R";
				Value = val;
				Course = new Dictionary<string, double>();
				Course["E"] = 0.011;
				Course["D"] = 0.013;
			}
		}

		public class Dollar : Money
		{
			public Dollar(double val)
			{
				Currency = "D";
				Value = val;
				Course = new Dictionary<string, double>();
				Course["E"] = 0.84;
				Course["R"] = 74.25;
			}
		}

		public class Euro : Money
		{
			public Euro(double val)
			{
				Currency = "E";
				Value = val;
				Course = new Dictionary<string, double>();
				Course["D"] = 1.19;
				Course["R"] = 88.72;

			}
		}

		public sealed class UnconvertableMoney : Money
		{
			new private Dictionary<string, double> Course;
			public UnconvertableMoney(double val, string curr)
			{
				Value = val;
				Currency = curr;
			}
		}
	}


