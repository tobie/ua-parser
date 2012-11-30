using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ua_parser;

namespace ua_parser_console
{
    class Program
    {
        static void Main(string[] args)
        {
            String uaString = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-I9300 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parser uaParser = new Parser();
            Client c = uaParser.Parse(uaString);
            sw.Stop();
            Console.WriteLine("Parsing the user agent string took " + sw.ElapsedMilliseconds + "ms\n");

            Console.WriteLine("Browser");
            Console.WriteLine(c.userAgent.family);
            Console.WriteLine(c.userAgent.major);
            Console.WriteLine(c.userAgent.minor);

            Console.WriteLine();
            Console.WriteLine("OS");
            Console.WriteLine(c.os.family);
            Console.WriteLine(c.os.major);
            Console.WriteLine(c.os.minor);


            Console.WriteLine();
            Console.WriteLine("device family: " + c.device.family);
            Console.WriteLine("isMobile? " + c.device.isMobile);

            Console.ReadLine();
        }
    }
}