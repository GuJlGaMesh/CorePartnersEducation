using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_02_04
{
	public class User : IComparable<User>
	{
		public  string FirstName { get; private set; }
		public  string LastName { get; private set; }
		public  DateTime LoginTime { get;}

		public User(string name, string lastName, DateTime t)
		{
			LoginTime = t;
			FirstName = name;
			LastName = lastName;
		}

		public int CompareTo(User? other)
		{
			return LoginTime.CompareTo(other.LoginTime);
		}

		public override string ToString()
		{
			return $"Name: {FirstName}; Last name: {LastName}; Login time; {LoginTime};";
		}
	}
}
