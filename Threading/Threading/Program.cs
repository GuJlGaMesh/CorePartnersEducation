using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
	class User
	{
		public string Name { get; set; }
		public List<User> _users = new List<User>();
		public List<User> CreateUsers(int count)
		{

			for (int i = 0; i < count; i++)
			{
				_users.Add(new User() { Name = i.ToString() + "i" + i.GetHashCode() });
			}

			return _users;
		}

		public static void DoWork(List<User> u)
		{
			u.ForEach(x =>
			{
				//Console.WriteLine($"Name: {x.Name}");
				Thread.Sleep(1);
			});

			;
		}
		public static void DoWorkParallel(List<User> u)
		{
			var options = new ParallelOptions()
			{
				MaxDegreeOfParallelism = 3
			};
			Parallel.ForEach<User>(u, options, x =>
			 {
				//Console.WriteLine($"Name: {x.Name}");
				Thread.Sleep(1);
			 });

			;
		}
		public static void DoWork(List<User> u, CancellationToken token)
		{
			u.ForEach(x =>
			{
				if (token.IsCancellationRequested)
					return;
				Console.WriteLine($"Name: {x.Name}");
				Thread.Sleep(1);
			});
		}
	}
	class Program
	{
		/*
			work without threads: 21251
			work with threads: 6972 
			all users collection by parallel do work time: 7015
			parallel invoke of parallel do works: 2346
			parallel invoke of ordinary do works: 7007
		 */
		static async Task Main(string[] args)
		{
			Parallel.Invoke(Block1,Block2);
		}

		public async static void Block2()
		{
			double a = 12312312, b = 123423;
			var task = new Task<double>(() => a / b);
			task.Start();
			task.Wait();
			Console.WriteLine("result of task running: " + task.Result);
			a = 2;
			b = 0;
			task = Task<double>.Run(() => a / b);
			try
			{
				task.Wait();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Task parent = new Task(() =>
			{
				var cts = new CancellationTokenSource();
				var tf = new TaskFactory<Int32>(cts.Token,
					TaskCreationOptions.AttachedToParent,
					TaskContinuationOptions.ExecuteSynchronously,
					TaskScheduler.Default);
				// Задание создает и запускает 3 дочерних задания
				var childTasks = new[]
				{
					tf.StartNew(() => Sum(cts.Token, 10000)),
					tf.StartNew(() => Sum(cts.Token, 20000)),
					tf.StartNew(() => Sum(cts.Token, Int32.MaxValue)) // Исключение
					// OverflowException
				};
			});
			parent.Start();
			Console.WriteLine("parent task started.");
			parent.Wait();
		}

		public static int Sum(CancellationToken c, int num)
		{
			for (int i = 0; i < num; i++) num += 0;
			Console.WriteLine(num + "  - task running with this num");
			return num;
		}
		public async static void Block1()
		{
			#region init

			var users = new User().CreateUsers(1500);
		var f = users.Take(500);
		var s = users.Skip(500).Take(500);
		var t = users.TakeLast(500);
		var sw = new Stopwatch();
		#endregion

		#region no threads


		sw.Start();
			User.DoWork(f.ToList());
			User.DoWork(s.ToList());
			User.DoWork(t.ToList());
			sw.Stop();

			Console.WriteLine(sw.ElapsedMilliseconds + " - work without threads.");

			#endregion

			#region yes threads

			sw.Reset();
			sw.Start();
			var t1 = new Thread(() => User.DoWork(f.ToList()));
		var t2 = new Thread(() => User.DoWork(s.ToList()));
		var t3 = new Thread(() => User.DoWork(t.ToList()));
		t1.Start();
			t2.Start();
			t3.Start();
			t1.Join();
			t2.Join();
			t3.Join();
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds + " - work with threads.");

			#endregion
			
			/*
			#region task threadspool

			var task = new Task(() =>
			{
				ThreadPool.QueueUserWorkItem((obj) => User.DoWork(f.ToList()));
				ThreadPool.QueueUserWorkItem((obj) => User.DoWork(s.ToList()));
				ThreadPool.QueueUserWorkItem((obj) => User.DoWork(t.ToList()));
			});
			task.Start();
			//task.Wait();
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds + " - threadpool excuted by task.");

			await Task.Delay(10000);
			Console.WriteLine("finish.");

			#endregion
			/*
			#region cancel token

			var cts = new CancellationTokenSource();
			var thread = new Thread(() => User.DoWork(users, cts.Token));
			thread.Start();
			cts.Token.Register(() => Console.WriteLine("Operation canceled after 10 seconds."));
			cts.CancelAfter(10000);

			#endregion
			//*/

			#region do work parallel

			sw.Restart();
			User.DoWorkParallel(users);
			sw.Stop();
			Console.WriteLine("all users collection by parallel do work time: " + sw.ElapsedMilliseconds);

			#endregion

			#region parallel invoke of parallel do work vs ordinary do work 
			sw.Restart();
			Parallel.Invoke(
				()=>User.DoWorkParallel(f.ToList()),
				()=>User.DoWorkParallel(s.ToList()),
				()=>User.DoWorkParallel(t.ToList())
			);
			sw.Stop();
			Console.WriteLine("parallel invoke of parallel do works: " + sw.ElapsedMilliseconds);

			sw.Restart();
			Parallel.Invoke(
				() => User.DoWork(f.ToList()),
				() => User.DoWork(s.ToList()),
				() => User.DoWork(t.ToList())
			);
			sw.Stop();
			Console.WriteLine("parallel invoke of ordinary do works: " + sw.ElapsedMilliseconds);
			#endregion


		}
}
}
