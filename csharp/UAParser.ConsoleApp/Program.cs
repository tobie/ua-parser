using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace UAParser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            String uaString = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-I9300 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";

            Parser uaParser = Parser.GetDefault();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            ClientInfo c = uaParser.Parse(uaString);
            sw.Stop();
            Console.WriteLine("Parsing the user agent string took " + sw.ElapsedMilliseconds + "ms\n");

            Console.WriteLine("Browser: ");
            Console.WriteLine(c.UserAgent);

            Console.WriteLine();
            Console.WriteLine("OS: ");
            Console.WriteLine(c.OS);


            Console.WriteLine();
            Console.WriteLine("Device: ");
            Console.WriteLine(c.Device);

            Console.ReadLine();
        }
    }
}
