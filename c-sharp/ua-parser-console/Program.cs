using System;
using ua_parser;

namespace ua_parser_console
{
    class Program
    {
        static void Main(string[] args)
        {
			String uaString = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/536.26.17 (KHTML, like Gecko) Version/6.0.2 Safari/536.26.17";
			
			Parser uaParser = new Parser();
			Client c = uaParser.Parse(uaString);

			Console.WriteLine(c.userAgent.family);
			Console.WriteLine(c.userAgent.major);
			Console.WriteLine(c.userAgent.minor);
			Console.WriteLine(c.userAgent.patch);
			Console.WriteLine(c.userAgent.ToString());

			Console.WriteLine();
			Console.WriteLine(c.os.family);
			Console.WriteLine(c.os.major);
			Console.WriteLine(c.os.minor);
			Console.WriteLine(c.os.ToString());
			
			
			Console.WriteLine();
			Console.WriteLine("device family: " + c.device.family);
			Console.WriteLine("computer? " + c.device.isComputer);
			Console.WriteLine("mobile? " + c.device.isMobile);
			Console.WriteLine("spider? " + c.device.isSpider);

        }
    }
}