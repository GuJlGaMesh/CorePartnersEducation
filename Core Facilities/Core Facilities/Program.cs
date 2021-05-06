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

    public class StaticCtorExapmle
    {
	    static StaticCtorExapmle()
	    {
		    try
		    {
			    throw new Exception("hello world!");
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    Console.WriteLine(e.Message);
		    }
		    finally
		    {
                Console.WriteLine("static constructor ended correctly.");
		    }
	    }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {

	        try
	        {
		        var t = new StaticCtorExapmle();
	        }
	        catch (Exception e)
	        {
		        Console.WriteLine(e);
		        throw;
	        }
            Console.WriteLine("code still works");
           /*
            #region primitive type exception catch
            var s = "text inside code";
            bool b = true;
            while (b)
            {
                try
                {
                    Console.WriteLine("input substr: ");
	                var s1 = Console.ReadLine();
                    Console.WriteLine("input integer: ");
                    int a = int.Parse(Console.ReadLine());
                   Console.WriteLine($"{a} - your input.");
                    Console.WriteLine("existing substring: " +  s.Substring(s.IndexOf(s1)));
                }
                catch (FormatException e)
                {
                    b = false;
                    Console.WriteLine("Format exception was thrown.");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    b = false;
                    Console.WriteLine("Argument out of blah blah exception.");
                }
                finally
                {
                    if (!b)
                        Console.WriteLine("An exception was thrown.");
                    else Console.WriteLine("Everything is going right.");
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
/*
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
