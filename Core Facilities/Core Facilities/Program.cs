using System;
using System.Globalization;
using System.IO;

namespace Core_Facilities
{
    public class MyAwesomeExceptionClass : Exception
    {
        public MyAwesomeExceptionClass(string message) : base(message)
        {
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            #region primitive type exception catch

            bool b = false;
            while (!b)
            {
                try
                {
                    int a = int.Parse(Console.ReadLine());
                    Console.WriteLine($"{a} - your input.");
                }
                catch (Exception e)
                {
                    b = true;
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (b)
                        Console.WriteLine("An exception was thrown.");
                    else Console.WriteLine("Everything is going right.");
                }
            }

            var r = new Random();
            Console.WriteLine("Guess one char and system let you go next.");
            bool guessed = false;
            var s = "";
            for (int i = 0; i < 5; i++)
            {
                s+=char.ConvertFromUtf32(r.Next(70, 80));
            }
            Console.WriteLine(s);
            var ii = 0;
            while (!guessed)
            {
                
                var guess = Console.ReadLine();
                try
                {
                    ii+=1;
                    Console.WriteLine(s.Substring(s.IndexOf(guess)));
                    guessed = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Console.WriteLine(ii + "try");
                }
            }

            #endregion

            #region my exception

            try
            {
                throw new MyAwesomeExceptionClass("My awesome exception was thrown.");
            }
            catch (MyAwesomeExceptionClass me)
            {
                Console.WriteLine(me.Message);
            }

            #endregion
//*/
            #region GC

            Console.WriteLine(GC.MaxGeneration + "max generation");
            Console.WriteLine(GC.CollectionCount(0) + " count of gc calls in 0-th generation");
            Console.WriteLine(GC.GetTotalMemory(false) + " bytes - total used memory");

            #endregion
//*/
            /*
            #region dispose
            using (var strReader = new StreamReader("file1.txt"))
            {
                string contents = strReader.ReadToEnd();
                Console.WriteLine(contents);
            }
            
            #endregion

            #region using
            StreamReader? streamReader = null;
            try
            {
                streamReader = new StreamReader("file1.txt");
                string contents = streamReader.ReadToEnd();
                Console.WriteLine(contents);
            }
            finally
            {
                streamReader?.Dispose();
            }
            

            #endregion
            //*/
        }
    }
}
