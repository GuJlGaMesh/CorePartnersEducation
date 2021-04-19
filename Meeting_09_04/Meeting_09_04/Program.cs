using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Meeting_09_04
{
	class Program
	{
		static void Main(string[] args)
		{
			#region char
			Console.WriteLine(char.IsDigit('1'));
			Console.WriteLine(char.IsLetterOrDigit('1'));
			Console.WriteLine(char.IsLetter('1'));
			Console.WriteLine(char.IsWhiteSpace(' '));
			#endregion

			#region @string 
			var s23 = @"
             _..-'(                         )`-.._  
           ./'. '||\\.        (\_/)       .//||` .`\.
        ./'.|'.'||||\\|..     )O O(    ..|//||||`.`|.`\.
     ./'..|'.|| |||||\``````  '`""'` ''''''/||||| ||.`|..`\.
  ./ '.||'.|||| ||||||||||||.       .|||||||||||| |||||.`||.`\.
 / '|||'.|||||| ||||||||||||{       }|||||||||||| ||||||.`|||`\
 '.|||'.||||||| ||||||||||||{       }|||||||||||| |||||||.`|||.`
'.||| ||||||||| |/'   ``\||``       ''||/''   `\| ||||||||| |||.`
|/ ' \./'     `\./         \!|\   /| !/         \./ '     `\./ `\|
V     V         V          }' `\ /' `{           V          V    V
`    `         `                V                '          '    '
";
			Console.WriteLine(s23);
			var s1 = "string";
			var s2 = "str";
			Console.WriteLine(s1.Equals(s2));
			Console.WriteLine(s1.CompareTo(s2));
			Console.WriteLine(s1.StartsWith(s2));
			Console.WriteLine(s1.EndsWith(s2));
			var s3 = (string)s2.Clone();
			s2 = "new str";
			Console.WriteLine(s3);
			var s4 = new char[s2.Length]; 
			s2.CopyTo(0, s4, 0, s2.Length);
			s4.ToList().ForEach(X => Console.WriteLine($"S4: {X}"));
			var s5 = s1.Substring(0, 3);
			Console.WriteLine(s5);
			#endregion

			Console.WriteLine();

			#region string builder
			StringBuilder sb = new StringBuilder("Просто фраза");
			sb.Append("!");
			sb.Insert(7, "обычная ");
			Console.WriteLine(sb);
			sb.Replace("фраза", "phrase");
			Console.WriteLine(sb);
			sb.Remove(7, 13);
			Console.WriteLine(sb);
			string s = sb.ToString();
			Console.WriteLine(s);
			#endregion

			#region iformatprovider
			DateTime dateValue = new DateTime(2009, 6, 1, 16, 37, 0);
			CultureInfo[] cultures = { new CultureInfo("en-US"),
				new CultureInfo("fr-FR"),
				new CultureInfo("it-IT"),
				new CultureInfo("de-DE") };
			foreach (CultureInfo culture in cultures)
				Console.WriteLine("{0}: {1}", culture.Name, dateValue.ToString(culture));
			#endregion

			#region string format
			long number = 88005353535;
			string result = String.Format("{0:+# (###) ###-##-##}", number);
			Console.WriteLine(result + " better call than borrow from someone!");
			Console.WriteLine(number + " better call than borrow from someone!");

			#endregion

			#region parse

			var ss = number.ToString();
			var parse = UInt64.Parse(ss);
			Console.WriteLine(parse + "- result of the parsing");
			#endregion

			#region Encoding
			string unicodeString = "This string contains the unicode character Pi (\u03a0)";
			Encoding ascii = Encoding.ASCII;
			Encoding unicode = Encoding.Unicode;
			byte[] unicodeBytes = unicode.GetBytes(unicodeString);
			byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
			char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
			ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
			string asciiString = new string(asciiChars);
			Console.WriteLine("Original string: {0}", unicodeString);
			Console.WriteLine("Ascii converted string: {0}", asciiString);
			// но так как у меня стоит русская кодировка в настройках консоли, то ничего не отображается 
			#endregion

			#region enum

			var p1 = new Person() {name = "Vitaliy", sex = Genders.male};
			var p2 = new Person() {name = "Marina", sex = Genders.female};
			var p3 = new Person() {name = "Already not Karina, but still no Sergey", sex = Genders.notProvided};
			Console.WriteLine(p1);
			Console.WriteLine(p2);
			Console.WriteLine(p3);

			#endregion

			#region jagged
			int[][] jaggedArray = new int[3][];
			jaggedArray[0] = new int[] { 1, 3, 5, 7, 9 };
			jaggedArray[1] = new int[] { 0, 2, 4, 6 };
			jaggedArray[2] = new int[] { 11, 22 };
			foreach (var arr in jaggedArray)
			{
				foreach (var a in arr)
				{
					Console.Write(a + " ");
				}
				Console.WriteLine();
			}
			#endregion
		}

		
	}

	#region enum

	// only two

	
	public enum Genders : ushort
	{
		male,
		female,
		notProvided
	}

	public struct Person
	{
		public string name;
		public Genders sex;
		public override string ToString()
		{
			return $"Name: {name} gender: {(sex.IsMale() || sex.IsFemale() ? $" is really {sex}" : "nevermind, pal")}";
		}
	}
	#endregion

	public static class EnumExtension
	{
		public static bool IsMale(this Genders gender) => gender.Equals(Genders.male);
		public static bool IsFemale(this Genders gender) => gender.Equals(Genders.female);
	}

}
