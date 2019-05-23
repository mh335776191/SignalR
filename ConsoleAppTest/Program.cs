using System;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var connection = new Connection("http://signalrtest2012.com/myhub");
            connection.Start().Wait();
            await connection.Send("");

        }
    }
}
 