using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_part_two
{
    class Program
    {
        private ConcurrentDictionary<int,int> cd = new ConcurrentDictionary<int,int>();
        void UpgradeDictionary()
        {
            for (int i = 0; i < 100; i++)
            {
                cd[i] = i*i;
                Console.WriteLine("Make product " + i * i);
            }
        }

        void ViewDictionary()
        {
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var val = r.Next(0, 100);
                int mult;
                if (cd.TryGetValue(val,out mult))
                    Console.WriteLine("Consume : " + mult);
                else Console.WriteLine($"Value {val} couldn't be consumed");
            }
        }

        public static async Task Main()
        {
            var p = new Program();
            /*
            #region 1

           
            try
            {
                Console.WriteLine(await p.Divide(3, 4));
                Console.WriteLine(await p.Divide(5, 0));
                Console.WriteLine(await p.Divide(6, 4));
                Console.WriteLine(await p.Divide(8, 4));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            #endregion

            //2
            Console.WriteLine(await p.Divide1(p.Divide(10,5)));
            
            #region 3

            var upgrade = new Task(p.UpgradeDictionary);
            var view = new Task(p.ViewDictionary);
            upgrade.Start();
            view.Start();
            try
            {
                Task.WaitAll(view, upgrade);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                upgrade.Dispose();
                view.Dispose();
            }

            #endregion
            //*/

            #region 4

            var users = User.CreateUsers(150);
            var first = users.Take(50).ToList();
            var second = users.Skip(50).Take(50).ToList();
            var third = users.TakeLast(50).ToList();

            var manager = new Manager();

            Parallel.Invoke(() => manager.DoWork1(first), () => manager.DoWork2(second), () => manager.DoWork3(third));
            #endregion
        }

        public async Task<int> Divide(int a, int b) => await Task.Run(() => a / b);
        public async Task<int> Divide1(Task<int> task) => await task.ConfigureAwait(false);
    }

    public class User
    {
        public string  Name { get; set; }

        public static List<User> CreateUsers(int count)
        {
            var options = new ParallelOptions()
                { MaxDegreeOfParallelism = 5 };
            var result = new List<User>();
            Parallel.For(0, count, options, (state) =>
            {
                var u = new User() { Name = "User #" + 1 };
                result.Add(u);
            });
            return result;
        }
    }

    public class Manager
    {
        private int _processedUserCount;

        private object lockMe = new object();

        private Mutex mutex = new Mutex();

        public void DoWork1(List<User> users)
        {
            users.ForEach(x =>
            {
                Thread.Sleep(100);
                Interlocked_LogProcessedCount();
            });
        }

        public void DoWork2(List<User> users)
        {
            users.ForEach(x =>
            {
                Thread.Sleep(100);
                Mutex_LogProcessedCount();
            });
        }

        public void DoWork3(List<User> users)
        {
            users.ForEach(x =>
            {
                Thread.Sleep(100);
                Lock_LogProcessedCount();
            });
        }

        public void Interlocked_LogProcessedCount()
        {
            _processedUserCount = Interlocked.Increment(ref _processedUserCount);
            Console.WriteLine($"~Interlocked~ Count: {_processedUserCount}");
        }

        public void Lock_LogProcessedCount()
        {
            lock (lockMe)
            {
                _processedUserCount++;
                Console.WriteLine($"~Lock~ Count: {_processedUserCount}");
            }
        }

        public void Mutex_LogProcessedCount()
        {
            mutex.WaitOne();
            _processedUserCount++;
            Console.WriteLine($"~Mutex~ Count: {_processedUserCount}");
            mutex.ReleaseMutex();
        }
    }
}
