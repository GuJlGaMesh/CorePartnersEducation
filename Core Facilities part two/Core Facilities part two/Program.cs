using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core_Facilities_part_two
{
	class MyClass
	{
		public int _a { get; set; }
	public MyClass() : this(0) { } 
	public MyClass (int a) { _a = a; }
		public void AnyMethod() => Console.WriteLine("AnyMethod was called");
	}
	[Serializable]
	class SquarePower : ISerializable
	{
		public int Number;
		[NonSerialized]
		int Square;
		public SquarePower (int a)
		{
			Number = a;
			Square = Number * Number;
		}
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Number", this.Number);
		}
		public SquarePower(SerializationInfo info, StreamingContext context)
		{
			Number = (int)info.GetValue("Number", typeof(int));
			Square = Number * Number;
		}
		override public string ToString()
		{
			return $"num: {Number} square: {Square}";
		}
	}

	class MySerializibleDateTime : ISerializationSurrogate
	{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Date", ((DateTime)obj).ToUniversalTime().ToString("u"));
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return DateTime.ParseExact(
				info.GetString("Date"), "u", null).ToLocalTime();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			#region reflection
			var t = typeof(MyClass);
			Console.WriteLine($"my own type is: {t}");
			var mc = (MyClass)Activator.CreateInstance(t);
			Console.WriteLine($"mc.a= {mc._a}");
			var mc1 = (MyClass)Activator.CreateInstance(t, new object[] { 10});
			Console.WriteLine($"mc1.a= {mc1._a}");
			var ti = t.GetTypeInfo();
			var method = ti.GetMethod("AnyMethod");
			method.Invoke(mc, null);
			var property = ti.GetProperty("_a");
			property.SetValue(mc, 15);
			Console.WriteLine($"New value of mc.a= {mc._a}");
			#endregion

			#region serialization
			var b = new BinaryFormatter();
			var s = new MemoryStream();
			var sp = new SquarePower(2);
			b.Serialize(s,sp);
			s.Position = 0;
			sp = b.Deserialize(s) as SquarePower;
			Console.WriteLine(sp);
			#endregion

			#region surrogate serialization

			using (var stream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				var ss = new SurrogateSelector();
				ss.AddSurrogate(typeof(DateTime), formatter.Context, new MySerializibleDateTime());
				formatter.SurrogateSelector = ss;
				DateTime localTimeBeforeSerialize = DateTime.Now;
				formatter.Serialize(stream, localTimeBeforeSerialize);
				stream.Position = 0;
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
				stream.Position = 0;
				DateTime localTimeAfterDeserialize =
					(DateTime)formatter.Deserialize(stream);
				Console.WriteLine(
					"LocalTimeBeforeSerialize ={0}", localTimeBeforeSerialize);
				Console.WriteLine(
					"LocalTimeAfterDeserialize={0}", localTimeAfterDeserialize);
			}
			#endregion
		}
	}
}
