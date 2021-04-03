
using System;

namespace Meeting_02_04
{
	public class SetValueOnce<T>
	{
		public bool _set;
		private T _value;

		public SetValueOnce()
		{
			_value = default(T);
			_set = false;
		}

		public SetValueOnce(T value)
		{
			_value = value;
			_set = true;
		}

		public T Value
		{
			get
			{
				if (!_set)
					Console.WriteLine("Value has not been set yet!");
				else
					return _value;

				return default(T);
			}
			set
			{
				if (_set)
					Console.WriteLine("Value already set!");
				else
				{
					_value = value;
					_set = true;
				}
				
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}

	public class Node<T> where T: struct
	{
		public T? _data;
		private Node<T> _next;
		public Node(T data) : this(data, null){}

		public Node(T data, Node<T> next)
		{
			_data = data;
			_next = next;
		}
		/*
		public void Add(Node<T> next)
		{
			var n = _next;
			while (n._next != null) n = n._next;
			n._next = next;
		}
		public void Add(T data) => _next = new Node<T>(data);
		*/
		public override string ToString()
		{
			return _data + "->" + (_next != null ? _next.ToString() : "null");
		}
	}
}
