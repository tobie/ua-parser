using System;
using System.Collections.Generic;
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

            Parser uaParser = new Parser();
            Client c = uaParser.Parse(uaString);

            Console.WriteLine("Browser");
            Console.WriteLine(c.userAgent.family); // => "Mobile Safari"
            Console.WriteLine(c.userAgent.major);  // => "5"
            Console.WriteLine(c.userAgent.minor);  // => "1"

            Console.WriteLine();
            Console.WriteLine("OS");
            Console.WriteLine(c.os.family);        // => "iOS"
            Console.WriteLine(c.os.major);         // => "5"
            Console.WriteLine(c.os.minor);         // => "1"


            Console.WriteLine();
            Console.WriteLine("device family: " + c.device.family);    // => "iPhone"
            Console.WriteLine("isMobile? " + c.device.isMobile);  // => true

            Console.ReadLine();
        }
    }
}