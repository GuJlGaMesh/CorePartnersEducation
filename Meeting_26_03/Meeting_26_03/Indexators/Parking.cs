using System;
using System.Collections.Generic;
using System.Linq;

namespace Meeting_26_03.Indexators
{
	class Parking
	{
		private List<Car> _cars = new List<Car>();
		public int MaxParkingSpaces { get; } = 100;
		public int Count => _cars.Count;
		public int this[Car car]
		{
			get
			{
				if (_cars.IndexOf(car) == -1)
				{
					Console.WriteLine("No matches at all");
					return -1;
				}

				return _cars.IndexOf(car);
			}
			set
			{
				var p = value;
				if (Count < p)
					_cars.Resize(p*2);
				_cars[p] = this[car.Number];

			}
		}
		public Car this[int position]
		{
			get
			{
				return _cars[position];
			}
			set
			{

			}
		}
		public Car this[string number]
		{
			get { return _cars.FirstOrDefault(x => x.Number == number); }
		}
		public void Add(Car car)
		{
			if (car != null && Count < MaxParkingSpaces)
				_cars.Add(car);
			else Console.WriteLine("No available parking spaces");
		}
	}
}
