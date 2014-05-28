// The file was automatically generated using a T4 Text Template.
//
// Source   : OSTests.g.tt
// Generated: Wed, 28 May 2014 16:24:47 GMT
// Generator: Microsoft.VisualStudio.TextTemplating.VSHost.12.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a

namespace UAParser.Tests.Generated
{
    using UAParser;
    using Xunit;
    
    // ReSharper disable InconsistentNaming

    namespace TestUserAgentParserOS 
    {
        // Following tests were generated automatically from test_user_agent_parser_os.yaml

        public class Android_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Android_Line_3() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-us; Silk/1.1.0-80) AppleWebKit/533.16 (KHTML, like Gecko) Version/5.0 Safari/533.16 Silk-Accelerated=true");
                Assert.Equal(@"Android", os.Family);
            }

            [Fact]
            public void Android_Line_10() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; en-us; KFTT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.0 Safari/535.19 Silk-Accelerated=false");
                Assert.Equal(@"Android", os.Family);
            }

            [Fact]
            public void Android_Line_17() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; en-us; KFTT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.2 Safari/535.19 Silk-Accelerated=true");
                Assert.Equal(@"Android", os.Family);
            }

            [Fact]
            public void Android_Line_24() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; en-us; KFOT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.1 Safari/535.19 Silk-Accelerated=true");
                Assert.Equal(@"Android", os.Family);
            }

            [Fact]
            public void Android_2_2_2_Line_31() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 2.2.2; en-gb; HTC Desire Build/FRG83G) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void Android_2_3_3_Line_38() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 2.3.3; en-fr; HTC/WildfireS/1.33.163.2 Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Android_2_3_4_Line_45() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 2.3.4; en-us; Kindle Fire Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Android_2_3_5_Line_52() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux;U;Android 2.3.5;en-us;TECNO T3 Build/master) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }

            [Fact]
            public void Android_3_0_1_Line_59() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 3.0.1; en-us; GT-P7510 Build/HRI83) AppleWebKit/534.13 (KHTML, like Gecko) Version/4.0 Safari/534.13");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void Android_3_2_Line_66() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Android 3.2; Linux; Opera Tablet/ADR-1106291546; U; en) Presto/2.8.149 Version/11.10");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_4_0_3_Line_73() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; KFTT Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Android_4_0_3_Line_80() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; KFOT Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Android_4_0_3_Line_87() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; Amaze_4G Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Android_4_0_4_Line_94() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 4.0.4; en-us; PJ83100/2.20.502.7 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.0");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Android_4_1_1_Line_101() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; Android 4.1.1; SPH-L710 Build/JRO03L) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void Android_4_2_Line_108() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; Android 4.2; Galaxy Nexus Build/JOP40C) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_4_0_4_Line_1004() 
            { 
                var os = Parser.ParseOS(@"JUC(Linux;U;Android4.0.4;Zh_cn;GT-S6012;240*320;)UCWEB7.8.0.95/139/351");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Android_2_2_Line_1123() 
            { 
                var os = Parser.ParseOS(@"HuaweiT8100_TD/1.0 Android/2.2 Release/12.25.2010 Browser/WAP2.0 Profile/MIDP-2.0 Configuration/CLDC-1.1 AppleWebKit/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_2_3_5_Line_1130() 
            { 
                var os = Parser.ParseOS(@"iBrowser/2.7 Lenovo-A288t_TD/S100 Linux/2.6.35 Android/2.3.5 Release/02.29.2012 Browser/AppleWebkit533.1 Mobile Safari/533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }

            [Fact]
            public void Android_2_1_update1_Line_1137() 
            { 
                var os = Parser.ParseOS(@"Layar/3.1 Android/2.1-update1 (Samsung GT-I5500)");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"update1", os.Patch);
            }

            [Fact]
            public void Android_2_3_5_Line_1144() 
            { 
                var os = Parser.ParseOS(@"Lenovo-A288t_TD/S100 Linux/2.6.35 Android/2.3.5 Release/02.29.2012 Browser/AppleWebkit533.1 Mobile Safari/533.1 FlyFlow/1.4");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }

            [Fact]
            public void Android_2_3_Line_1151() 
            { 
                var os = Parser.ParseOS(@"MOT-MT870_TD/1.0 Android/2.3 (Linux; Android) Release/5.31.2011 Browser/WAP 2.0 (AppleWebKit 533.1) Profile/MIDP-2.0 Configuration/CLDC-1.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
            }

            [Fact]
            public void Android_2_1_Line_1158() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/21 Mozilla/5.0 (HW-HUAWEI_C8500/C8500V100R001C92B234;U;Android/2.1;240*320;CTC/2.0) AppleWebKit/530.17 Mobile Safari/530.17");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"1", os.Minor);
            }

            [Fact]
            public void Android_2_2_Line_1165() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/2.7/ZTE-TU880_TD/1.0 Linux/2.6.32 Android/2.2 Release/5.25.2011 Browser/AppleWebKit533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_2_1_Line_1172() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/3.1/Mozilla/5.0 (HW-C8600/C8600V100R001C92B225;U;Android/2.1;320*480;CTC/2.0) AppleWebKit/530.17 (KHTML, like Gecko) Version/4.0 Mobile Safari/530.17");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"1", os.Minor);
            }

            [Fact]
            public void Android_4_0_Line_1179() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/3.7/ZTEU795_TD/1.0 Linux/2.6.39 Android/4.0 Release/6.10.2012 Browser/AppleWebKit534.30");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Android_2_3_6_Line_1186() 
            { 
                var os = Parser.ParseOS(@"SAMSUNG-GT-I8250_TD/1.0 Android/2.3.6 Release/11.11.2011 Browser/AppleWebKit533.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"6", os.Patch);
            }

            [Fact]
            public void Android_4_0_4_Line_1193() 
            { 
                var os = Parser.ParseOS(@"Samsung-mini7100/1.0 Linux/2.6.35.7 Android/4.0.4 Release/05.18.2013 Browser/AppleWebKit533.1 (KHTML, Like Gecko) Mozilla/5.0 Mobile");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Android_2_2_Line_1200() 
            { 
                var os = Parser.ParseOS(@"ZTE-TU802_TD/1.0 Linux/2.6.32 Android/2.2 Release/1.1.2011 Browser/AppleWebKit530.17 Profile/MIDP-2.0 Configuration/CLDC-1.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_2_3_Line_1207() 
            { 
                var os = Parser.ParseOS(@"ZTEU880E_TD/1.0 Linux/2.6.35 Android/2.3 Release/12.15.2011 Browser/AppleWebKit533.1 baiduboxapp/4.8 (Baidu; P1 2.3.7)");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
            }

            [Fact]
            public void Android_2_3_6_Line_1403() 
            { 
                var os = Parser.ParseOS(@"JUC (Linux; U; 2.3.6; zh-cn; GT-S7500; 320*480) UCWEB7.9.0.94/139/352");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"6", os.Patch);
            }

            [Fact]
            public void Android_4_0_3_Line_1410() 
            { 
                var os = Parser.ParseOS(@"JUC (Linux; U; 4.0.3; zh-cn; NOVO7_Mars; 1024*552) UCWEB1.0.1.113/147/800");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Android_4_0_4_Line_1417() 
            { 
                var os = Parser.ParseOS(@"JUC(Linux;U;4.0.4;Zh_cn;GT-I9308;720*1280;)UCWEB7.7.0.85/139/999");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Android_2_2_1_Line_1424() 
            { 
                var os = Parser.ParseOS(@"JUC(Linux;U;Android2.2.1;Zh_cn;GT-S5570;240*320;)UCWEB7.8.0.95/140/350");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void Android_2_2_1_Line_1431() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (Linux; U; Adr 2.2.1; en-US; Dell Streak) U2/1.0.0 UCBrowser/9.3.1.344 U2/1.0.0 Mobile");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void Android_2_3_6_Line_1438() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (Linux; U; Adr 2.3.6; en-US; HUAWEI_Y210-0251) U2/1.0.0 UCBrowser/8.6.0.318 U2/1.0.0 Mobile");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"6", os.Patch);
            }

            [Fact]
            public void Android_4_2_2_Line_1445() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (Linux; U; Adr 4.2.2; zh-CN; ZTE V818) U2/1.0.0 UCBrowser/9.3.2.349 U2/1.0.0 Mobile");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }
        }
        public class Bada_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Bada_1_0_Line_115() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (SAMSUNG; SAMSUNG-GT-S8500/S8500XXJEE; U; Bada/1.0; nl-nl) AppleWebKit/533.1 (KHTML, like Gecko) Dolfin/2.0 Mobile WVGA SMM-MMS/1.2.0 OPN-B");
                Assert.Equal(@"Bada", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Bada_Line_1116() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Bada; Opera Mini/6.5/32.855; U; en) Presto/2.8.119 Version/11.10");
                Assert.Equal(@"Bada", os.Family);
            }
        }
        public class BlackBerry_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void BlackBerry_OS_10_0_9_Line_122() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (BB10; Touch) AppleWebKit/537.3+ (KHTML, like Gecko) Version/10.0.9.388 Mobile Safari/537.3+");
                Assert.Equal(@"BlackBerry OS", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"9", os.Patch);
            }

            [Fact]
            public void BlackBerry_OS_6_0_0_Line_129() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (BlackBerry; U; BlackBerry 9800; en-GB) AppleWebKit/534.1+ (KHTML, like Gecko) Version/6.0.0.141 Mobile Safari/534.1+");
                Assert.Equal(@"BlackBerry OS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"0", os.Patch);
                Assert.Equal(@"141", os.PatchMinor);
            }

            [Fact]
            public void BlackBerry_OS_6_0_0_Line_136() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (BlackBerry; U; BlackBerry 9800; en-US) AppleWebKit/534.1  (KHTML, like Gecko) Version/6.0.0.91 Mobile Safari/534.1 ");
                Assert.Equal(@"BlackBerry OS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"0", os.Patch);
                Assert.Equal(@"91", os.PatchMinor);
            }

            [Fact]
            public void BlackBerry_OS_Line_143() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (BlackBerry; Opera Mini/7.0.31437/28.3030; U; en) Presto/2.8.119 Version/11.10");
                Assert.Equal(@"BlackBerry OS", os.Family);
            }
        }
        public class BlackBerry_Tablet_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void BlackBerry_Tablet_OS_1_0_0_Line_150() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (PlayBook; U; RIM Tablet OS 1.0.0; en-US) AppleWebKit/534.8+ (KHTML, like Gecko) Version/0.0.1 Safari/534.8+");
                Assert.Equal(@"BlackBerry Tablet OS", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"0", os.Patch);
            }
        }
        public class BREW_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void BREW_Line_157() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (BREW; Opera Mini/5.1.191/27.2202; U; en) Presto/2.8.119 240X400 LG VN271");
                Assert.Equal(@"BREW", os.Family);
            }

            [Fact]
            public void BREW_3_1_5_Line_164() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (BREW 3.1.5; U; en-us; Sanyo; NetFront/3.5.1/AMB) Boost SCP3810");
                Assert.Equal(@"BREW", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }

            [Fact]
            public void BREW_3_1_5_Line_171() 
            { 
                var os = Parser.ParseOS(@"NetFront/3.5.1 (BREW 3.1.5; U; en-us; LG; NetFront/3.5.1/WAP) Sprint LN240 MMP/2.0 Profile/MIDP-2.1 Configuration/CLDC-1.1");
                Assert.Equal(@"BREW", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }
        }
        public class Brew_MP_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Brew_MP_1_0_2_Line_178() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (Brew MP 1.0.2; U; en-us; Sanyo; NetFront/3.5.1/AMB) Sprint E4100");
                Assert.Equal(@"Brew MP", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void Brew_MP_1_0_4_Line_185() 
            { 
                var os = Parser.ParseOS(@"NetFront/4.2 (BMP 1.0.4; U; en-us; LG; NetFront/4.2/AMB) Boost LG272 MMP/2.0 Profile/MIDP-2.1 Configuration/CLDC-1.1");
                Assert.Equal(@"Brew MP", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Brew_MP_1_0_2_Line_192() 
            { 
                var os = Parser.ParseOS(@"PantechP6010/JNUS11072011 BMP/1.0.2 DeviceId/141020 NetFront/4.1 OMC/1.5.3 Profile/MIDP-2.1 Configuration/CLDC-1.1");
                Assert.Equal(@"Brew MP", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }
        }
        public class BSD_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void BSD_Line_199() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; BSD Four; en-US) AppleWebKit/533.3 (KHTML, like Gecko) rekonq Safari/533.3");
                Assert.Equal(@"BSD", os.Family);
            }
        }
        public class Debian_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Debian_Line_206() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1.16) Gecko/20110302 Conkeror/0.9.2 (Debian-0.9.2+git100804-1)");
                Assert.Equal(@"Debian", os.Family);
            }
        }
        public class Firefox_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Firefox_OS_Line_213() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Mobile; rv:15.0) Gecko/15.0 Firefox/15.0");
                Assert.Equal(@"Firefox OS", os.Family);
            }

            [Fact]
            public void Firefox_OS_Line_220() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Tablet; rv:29.0) Gecko/29.0 Firefox/29.0");
                Assert.Equal(@"Firefox OS", os.Family);
            }
        }
        public class iOS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void iOS_3_2_Line_227() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B367 Safari/531.21.10");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void iOS_4_3_2_Line_234() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPod; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"3", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void iOS_6_0_1_Line_241() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/371 Mozilla/5.0 (iPhone 4S; CPU iPhone OS 6_0_1 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Mobile/10A523 Safari/7534.48.3");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void iOS_5_1_1_Line_458() 
            { 
                var os = Parser.ParseOS(@"IUC(U;iOS 5.1.1;Zh-cn;320*480;)/UCWEB7.9.0.94/41/997");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void iOS_6_1_4_Line_1095() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPhone; U; CPU iPhone 6_1_4 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Mobile/7E18 Grindr/1.8.8 (iPhone5,2/6.1.4)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void iOS_7_0_Line_1102() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPhone; U; CPU iPhone 7_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Mobile/7E18 Grindr/1.8.8 (iPhone3,1/7.0)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void iOS_7_0_Line_1109() 
            { 
                var os = Parser.ParseOS(@"iTube 2.15 (iPhone; iPhone OS 7.0; en_US)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void iOS_6_1_3_Line_1466() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (iOS; U; iPad OS 6_1_3; en-US; iPad2,5) U2/1.0.0 UCBrowser/9.0.0.260 U2/1.0.0 Mobile");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void iOS_5_0_1_Line_1473() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (iOS; U; iPh OS 5_0_1; zh-CN; iPh4,1) U2/1.0.0 UCBrowser/9.0.1.284 U2/1.0.0 Mobile");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void iOS_7_0_4_Line_1480() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (iOS; U; iPh OS 7_0_4; ru; iPh3,1) U2/1.0.0 UCBrowser/9.0.0.260 U2/1.0.0 Mobile");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void iOS_7_0_4_Line_1487() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (iOS; U; iPh OS 7_0_4; zh-CN; iPh4,1) U2/1.0.0 UCBrowser/9.0.1.284 U2/1.0.0 Mobile");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void iOS_5_Line_1739() 
            { 
                var os = Parser.ParseOS(@"DEPoker-iPad/1.0.2 CFNetwork/548.1.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_7_Line_1746() 
            { 
                var os = Parser.ParseOS(@"JDSports-iPad/1.1 CFNetwork/672.0.8 Darwin/14.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1753() 
            { 
                var os = Parser.ParseOS(@"AngryBirdsBlack-iPhone/1.1.0 CFNetwork/548.1.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1760() 
            { 
                var os = Parser.ParseOS(@"Bing for iPad/1.1.2 CFNetwork/485.13.9 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1767() 
            { 
                var os = Parser.ParseOS(@"NightstandPaid-iPad/1.3.1 CFNetwork/548.1.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_7_Line_1774() 
            { 
                var os = Parser.ParseOS(@"Glo-De-iPad/1.4.7 CFNetwork/672.0.2 Darwin/14.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
            }

            [Fact]
            public void iOS_7_Line_1781() 
            { 
                var os = Parser.ParseOS(@"Island for iPhone/1.95 CFNetwork/672.0.2 Darwin/14.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1788() 
            { 
                var os = Parser.ParseOS(@"WormsiPhone-iPad/2.3 CFNetwork/548.1.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_6_Line_1795() 
            { 
                var os = Parser.ParseOS(@"Rummy LITE iPad/2.3.0 CFNetwork/609.1.4 Darwin/13.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
            }

            [Fact]
            public void iOS_4_Line_1802() 
            { 
                var os = Parser.ParseOS(@"MobileRSSFree-iPad/3.1 CFNetwork/467.12 Darwin/10.3.1");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"4", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1809() 
            { 
                var os = Parser.ParseOS(@"MobileRSSFree-iPad/3.1.4 CFNetwork/485.13.9 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_7_Line_1816() 
            { 
                var os = Parser.ParseOS(@"babbelIndonesian-iPad/4.0.1 CFNetwork/672.0.8 Darwin/14.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
            }

            [Fact]
            public void iOS_6_Line_1823() 
            { 
                var os = Parser.ParseOS(@"WeltMobile-iPad/4.2 CFNetwork/609.1.4 Darwin/13.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1830() 
            { 
                var os = Parser.ParseOS(@"IMPlusFull-iPad/7.9.1 CFNetwork/548.0.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_1_Line_1837() 
            { 
                var os = Parser.ParseOS(@"Cooliris/1.3 CFNetwork/342.1 Darwin/9.4.1");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"1", os.Major);
            }

            [Fact]
            public void iOS_4_Line_1844() 
            { 
                var os = Parser.ParseOS(@"Poof/1.0 CFNetwork/485.12.7 Darwin/10.4.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"4", os.Major);
            }

            [Fact]
            public void iOS_5_Line_1851() 
            { 
                var os = Parser.ParseOS(@"Parking Mania Free/1.9.5.0 CFNetwork/548.0.4 Darwin/11.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"5", os.Major);
            }

            [Fact]
            public void iOS_6_Line_1858() 
            { 
                var os = Parser.ParseOS(@"Planet Boing!/1.4.8 CFNetwork/609.1.4 Darwin/13.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
            }

            [Fact]
            public void iOS_7_Line_1865() 
            { 
                var os = Parser.ParseOS(@"PlayTube/1.7 CFNetwork/672.0.2 Darwin/14.0.0");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
            }

            [Fact]
            public void iOS_6_1_2_Line_1872() 
            { 
                var os = Parser.ParseOS(@"Appcelerator Titanium/2.1.2.GA (iPhone/6.1.2; iPhone OS; de_DE;)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void iOS_7_0_2_Line_1879() 
            { 
                var os = Parser.ParseOS(@"4 Pics 1 Word/3.9 (iPad; iOS 7.0.2; Scale/2.00)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void iOS_7_0_2_Line_1886() 
            { 
                var os = Parser.ParseOS(@"4 Pics 1 Word/3.9 (iPhone; iOS 7.0.2; Scale/2.00)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void iOS_6_1_Line_1893() 
            { 
                var os = Parser.ParseOS(@"server-bag [iPhone OS,6.1,10B144,iPhone3,2]");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
            }

            [Fact]
            public void iOS_6_1_2_Line_1900() 
            { 
                var os = Parser.ParseOS(@"IUC(U;iOS 6.1.2;Zh-cn;320*480;)/UCWEB8.2.1.132/42/997");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void iOS_7_0_Line_1907() 
            { 
                var os = Parser.ParseOS(@"StoreKitUIService/1.0 iOS/7.0 model/iPhone3,1 (4; dt:27)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void iOS_7_0_4_Line_1914() 
            { 
                var os = Parser.ParseOS(@"Yahoo!/9184 (iPhone; iOS 7.0.4; Scale/2.00)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void iOS_7_0_Line_1921() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPhone; U; CPU iPhone OS  7_0 like Mac OS X; tr_TR)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void iOS_7_0_2_Line_1928() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (iPhone; U; CPU iPhone OS  7_0_2 like Mac OS X; de_DE)");
                Assert.Equal(@"iOS", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }
        }
        public class Kindle_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Kindle_1_0_Line_248() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; Linux 2.6.10) NetFront/3.3 Kindle/1.0 (screen 600x800)");
                Assert.Equal(@"Kindle", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Kindle_1_0_Line_255() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; Linux 2.6.10) NetFront/3.3 Kindle/1.0 (screen 600x800)");
                Assert.Equal(@"Kindle", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Kindle_3_0_Line_262() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; en-US) AppleWebKit/528.5+ (KHTML, like Gecko, Safari/528.5+) Version/4.0 Kindle/3.0 (screen 600x800; rotate)");
                Assert.Equal(@"Kindle", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
            }
        }
        public class Linux_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Linux_Line_269() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux x86_64) AppleWebKit/534.26+ WebKitGTK+/1.4.1 luakit/f3a2dbe");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_276() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux; de-DE) AppleWebKit/527  (KHTML, like Gecko, Safari/419.3) konqueror/4.3.1");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_2_6_Line_283() 
            { 
                var os = Parser.ParseOS(@"python-requests/0.14 CPython/2.6 Linux/2.6-43-server");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
            }

            [Fact]
            public void Linux_Line_290() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux x86_64; rv:2.0) Gecko/20110408 conkeror/0.9.3");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_297() 
            { 
                var os = Parser.ParseOS(@"Midori/0.2 (X11; Linux; U; en-us) WebKit/531.2 ");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_304() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux x86_64; rv:2.0) Gecko/20110417 IceCat/4.0");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_311() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux i686 (x86_64); rv:2.0b4) Gecko/20100818 Firefox/4.0b4");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_318() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux x86_64; rv:2.0b8pre) Gecko/20101031 Firefox-4.0/4.0b8pre");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_Line_325() 
            { 
                var os = Parser.ParseOS(@"QQBrowser (Linux; U; zh-cn; HTC Hero Build/FRF91)");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_2_6_16_Line_1214() 
            { 
                var os = Parser.ParseOS(@"ELinks (0.10.6; Linux 2.6.16-hardened-r10 i686; 80x25)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"16", os.Patch);
            }

            [Fact]
            public void Linux_2_6_28_Line_1235() 
            { 
                var os = Parser.ParseOS(@"FeedDaemon FRITZ!OS/06.01 Linux/2.6.28.10");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"28", os.Patch);
            }

            [Fact]
            public void Linux_2_4_18_Line_1242() 
            { 
                var os = Parser.ParseOS(@"Links (0.99pre4; Linux 2.4.18-26.7.x i686; 119x44)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"4", os.Minor);
                Assert.Equal(@"18", os.Patch);
            }

            [Fact]
            public void Linux_2_4_2_Line_1256() 
            { 
                var os = Parser.ParseOS(@"Mozilla/3.0 (X11; U; Linux 2.4.2-2 i586; en-US; m18) Gecko/20010131 Netscape6/6.01");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"4", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }

            [Fact]
            public void Linux_2_2_Line_1263() 
            { 
                var os = Parser.ParseOS(@"Mozilla/3.01 (compatible; Netgem/3.6.8; netbox; Linux 2.2)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Linux_2_2_17_Line_1277() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 5.0; Linux 2.2.17-14 i586) Opera 6.0  [en]");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"17", os.Patch);
            }

            [Fact]
            public void Linux_2_2_12_Line_1298() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.61 [en] (X11; U; Linux 2.2.12-20 i686)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"12", os.Patch);
            }

            [Fact]
            public void Linux_2_6_8_Line_1305() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; Konqueror/3.2; Linux 2.6.8.1; X11; i686; en_US) (KHTML, like Gecko)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"8", os.Patch);
            }

            [Fact]
            public void Linux_2_6_27_Line_1319() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; Konqueror/3.5; Linux 2.6.27.47-TSDX; X11; x86_64; es) KHTML/3.5.7 (like Gecko),gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"27", os.Patch);
            }

            [Fact]
            public void Linux_Line_1361() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.2.10) Gecko/20100923 Caixa Magica Linux/1.9.2.10-0.1xcm15 (15) Firefox/3.6.10,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Linux", os.Family);
            }

            [Fact]
            public void Linux_2_2_22_Line_1375() 
            { 
                var os = Parser.ParseOS(@"Opera/7.10 (Linux 2.2.22 i686; U)  [en]");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"22", os.Patch);
            }

            [Fact]
            public void Linux_3_5_0_Line_1396() 
            { 
                var os = Parser.ParseOS(@"python-requests/1.2.3 CPython/2.7.3 Linux/3.5.0-23-generic");
                Assert.Equal(@"Linux", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"5", os.Minor);
                Assert.Equal(@"0", os.Patch);
            }
        }
        public class Mac_OS_X_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Mac_OS_X_Line_332() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en-us) AppleWebKit/418.8 (KHTML, like Gecko) Safari/419.3");
                Assert.Equal(@"Mac OS X", os.Family);
            }

            [Fact]
            public void Mac_OS_X_10_5_7_Line_339() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_5_7; en-us) AppleWebKit/530.17 (KHTML, like Gecko) Version/4.0 Safari/530.17 Skyfire/2.0");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"5", os.Minor);
                Assert.Equal(@"7", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_6_5_Line_346() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_5; en-us) AppleWebKit/533.18.1 (KHTML, like Gecko) Version/5.0.2 Safari/533.18.5");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_5_7_Line_1704() 
            { 
                var os = Parser.ParseOS(@"Safari5530.17 CFNetwork/438.12 Darwin/9.7.0 (i386) (Macmini2,1)");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"5", os.Minor);
                Assert.Equal(@"7", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_6_4_Line_1711() 
            { 
                var os = Parser.ParseOS(@"Safari/6533.18.5 CFNetwork/454.9.8 Darwin/10.4.0 (i386) (MacBookPro7,1)");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_7_4_Line_1718() 
            { 
                var os = Parser.ParseOS(@"Safari/7536.30.1 CFNetwork/520.5.1 Darwin/11.4.2 (i386) (MacBook3,1)");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"7", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_8_3_Line_1725() 
            { 
                var os = Parser.ParseOS(@"Reader Notifier/5 CFNetwork/596.3.3 Darwin/12.3.0 (x86_64) (MacBookPro7,1)");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"8", os.Minor);
                Assert.Equal(@"3", os.Patch);
            }

            [Fact]
            public void Mac_OS_X_10_9_0_Line_1732() 
            { 
                var os = Parser.ParseOS(@"Safari/9537.71 CFNetwork/673.0.2 Darwin/13.0.1 (x86_64) (MacBookPro11,1)");
                Assert.Equal(@"Mac OS X", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"9", os.Minor);
                Assert.Equal(@"0", os.Patch);
            }
        }
        public class Mac_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Mac_OS_Line_353() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 5.17; Mac_PowerPC)");
                Assert.Equal(@"Mac OS", os.Family);
            }
        }
        public class MeeGo_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void MeeGo_Line_360() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                Assert.Equal(@"MeeGo", os.Family);
            }
        }
        public class Nokia_Series_40_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Nokia_Series_40_Line_367() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Series40; NokiaC2-03/07.48; Profile/MIDP-2.1 Configuration/CLDC-1.1) Gecko/20100401 S40OviBrowser/2.2.0.0.33");
                Assert.Equal(@"Nokia Series 40", os.Family);
            }

            [Fact]
            public void Nokia_Series_40_Line_374() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Series40; NokiaX2-05/08.35; Profile/MIDP-2.1 Configuration/CLDC-1.1) Gecko/20100401 S40OviBrowser/2.0.2.68.14");
                Assert.Equal(@"Nokia Series 40", os.Family);
            }
        }
        public class Other_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Other_Line_381() 
            { 
                var os = Parser.ParseOS(@"ALCATEL-OT510A/382 ObigoInternetBrowser/Q05A");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_388() 
            { 
                var os = Parser.ParseOS(@"Alcatel-OH5/1.0 UP.Browser/6.1.0.7.7 (GUI) MMP/1.0");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_395() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_402() 
            { 
                var os = Parser.ParseOS(@"Bunjalloo/0.7.6(Nintendo DS;U;en)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_409() 
            { 
                var os = Parser.ParseOS(@"Opera/9.50 (Nintendo DSi; Opera/507; U; en-US)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_416() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Nintendo 3DS; U; ; en) Version/1.7498.US");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_423() 
            { 
                var os = Parser.ParseOS(@"Opera/9.30 (Nintendo Wii; U; ; 3642; en)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_430() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Nintendo WiiU) AppleWebKit/534.52 (KHTML, like Gecko) NX/2.1.0.8.21 NintendoBrowser/1.0.0.7494.US");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_437() 
            { 
                var os = Parser.ParseOS(@"NOKIA6120c/UC Browser7.4.0.65/28/352");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_444() 
            { 
                var os = Parser.ParseOS(@"Nokia5320di/UCWEB8.0.3.99/28/999");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_451() 
            { 
                var os = Parser.ParseOS(@"Nokia201/2.0 (11.21) Profile/MIDP-2.1 Configuration/CLDC-1.1 Mozilla/5.0 (Java; U; en-us; nokia201) UCBrowser8.3.0.154/70/355/UCWEB Mobile");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_465() 
            { 
                var os = Parser.ParseOS(@"J2ME/UCWEB7.0.3.45/139/7682");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_472() 
            { 
                var os = Parser.ParseOS(@"PantechP7040/JLUS04042011 Browser/Obigo/Q05A OMC/1.5.3 Profile/MIDP-2.1 Configuration/CLDC-1.1");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_479() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (PLAYSTATION 3; 3.55)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_486() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (PLAYSTATION 3 4.31) AppleWebKit/531.22.8 (KHTML, like Gecko)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_493() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (PSP (PlayStation Portable); 2.00)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_500() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (PlayStation Vita 1.81) AppleWebKit/531.22.8 (KHTML, like Gecko) Silk/3.2");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_507() 
            { 
                var os = Parser.ParseOS(@"Mozilla/3.0 (Planetweb/2.100 JS SSL US; Dreamcast US)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_514() 
            { 
                var os = Parser.ParseOS(@"HUAWEI-M750/001.00 ACS-NetFront/3.2");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_521() 
            { 
                var os = Parser.ParseOS(@"OneBrowser/3.0 (SAMSUNG-GT-S5253/S5253DDKJ2)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_528() 
            { 
                var os = Parser.ParseOS(@"OneBrowser/3.0 (NokiaC2-00/03.42)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_535() 
            { 
                var os = Parser.ParseOS(@"Huawei/1.0/0HuaweiG2800/WAP2.0/Obigo-Browser/Q03C MMS/Obigo-MMS/1.2");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_542() 
            { 
                var os = Parser.ParseOS(@"iBrowser/Mini2.8 (Nokia5130c-2/07.97)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_549() 
            { 
                var os = Parser.ParseOS(@"SAMSUNG-C3053/1.0 Openwave/6.2.3 Profile/MIDP-2.0 Configuration/CLDC-1.1 UP.Browser/6.2.3.3.c.1.101 (GUI) MMP/2.0");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_556() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (LG-T500 AppleWebkit/531 Browser/Phantom/V2.0 Widget/LGMW/3.0 MMS/LG-MMS-V1.0/1.2 Java/ASVM/1.1 Profile/MIDP-2.1 Configuration/CLDC-1.1)");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_563() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 YottaaMonitor;");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_570() 
            { 
                var os = Parser.ParseOS(@"SomethingWeNeverKnewExisted");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_1018() 
            { 
                var os = Parser.ParseOS(@"SendoS600/04 UP.Link/6.3.0.0.0");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_1025() 
            { 
                var os = Parser.ParseOS(@"SonyEricssonS600i/R4AB Browser/NetFront/3.3 Profile/MIDP-2.0 Configuration/CLDC-1.1");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_1032() 
            { 
                var os = Parser.ParseOS(@"Spice S6005/WAP2.0 IAC/M6226 Profile/MIDP-2.0 Configuration/CLDC-1.1");
                Assert.Equal(@"Other", os.Family);
            }

            [Fact]
            public void Other_Line_1039() 
            { 
                var os = Parser.ParseOS(@"Toshiba TS608_TS30/v1.0 UP.Browser/6.2.3.9.d.1 (GUI) MMP/2.0");
                Assert.Equal(@"Other", os.Family);
            }
        }
        public class Solaris_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Solaris_Line_577() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; SunOS i86pc; en-US; rv:1.8.0.5) Gecko/20060728 Firefox/1.5.0.5");
                Assert.Equal(@"Solaris", os.Family);
            }
        }
        public class Symbian_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_OS_Line_584() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Series 60; Opera Mini/6.24455/25.677; U; fr) Presto/2.5.25 Version/10.54");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_591() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (S60; SymbOS; Opera Mobi/275; U; es-ES) Presto/2.4.13 Version/10.00");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_598() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (S60V5; U; en-us; NokiaC5-03) AppleWebKit/530.13 (KHTML, like Gecko) UCBrowser/8.7.0.218/50/352/UCWEB Mobile");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_9_4_Line_605() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (SymbianOS/9.4; U; Series60/5.0 Nokia5800d-1/21.0.025; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/413 (KHTML, like Gecko) Safari/413");
                Assert.Equal(@"Symbian OS", os.Family);
                Assert.Equal(@"9", os.Major);
                Assert.Equal(@"4", os.Minor);
            }

            [Fact]
            public void Symbian_OS_9_4_Line_1011() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (SymbianOS 9.4; Series60/5.0 NokiaN97-1/10.0.012; Profile/MIDP-2.1 Configuration/CLDC-1.1; en-us) AppleWebKit/525 (KHTML, like Gecko) WicKed/7.1.12344");
                Assert.Equal(@"Symbian OS", os.Family);
                Assert.Equal(@"9", os.Major);
                Assert.Equal(@"4", os.Minor);
            }

            [Fact]
            public void Symbian_OS_Line_1046() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0(Symbian; U; S60 V5; en-US; Nokia808 PureView) U2/1.0.0 UCBrowser/8.8.0.245 U2/1.0.0 Mobile");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1053() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (S60; SymbOS; Opera Mobi/SYB-1204232256; U; en-GB) Presto/2.10.254 Version/12.00");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1060() 
            { 
                var os = Parser.ParseOS(@"Nokia5228/40.1.003/sw_platform=S60;sw_platform_version=5.0;java_build_version=1.4.48");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1067() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (S60V5; U; ru; NokiaN97 mini)/UC Browser8.5.0.183/50/352/UCWEB Mobile");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1074() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (S60V3; U; ru; Nokia N96)/UC Browser8.5.0.183/28/444/UCWEB Mobile");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1081() 
            { 
                var os = Parser.ParseOS(@"MQQBrowser/Mini2.2 (Nokia5230/50.0.001/sw_platform=S60;sw_platform_version=5.0;java_build_version=1.4.43)");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_Line_1088() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 5.0; S60/3.0 NOKIAN73/1.0 Profile/MIDP-2.0 Configuration/CLDC-1.1)");
                Assert.Equal(@"Symbian OS", os.Family);
            }
        }
        public class Symbian_3_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_3_Line_612() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Symbian/3; Series60/5.2 NokiaN8-00/013.016; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/525 (KHTML, like Gecko) Version/3.0 BrowserNG/7.2.8.10 3gpp-gba");
                Assert.Equal(@"Symbian^3", os.Family);
            }
        }
        public class Symbian_3_Anna_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_3_Anna_Line_619() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Symbian/3; Series60/5.2 NokiaN8-00/012.002; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/533.4 (KHTML, like Gecko) NokiaBrowser/7.3.0 Mobile Safari/533.4 3gpp-gba");
                Assert.Equal(@"Symbian^3 Anna", os.Family);
            }
        }
        public class Symbian_3_Belle_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_3_Belle_Line_626() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Symbian/3; Series60/5.3 Nokia701/111.020.0307; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/533.4 (KHTML, like Gecko) NokiaBrowser/7.4.1.14 Mobile Safari/533.4 3gpp-gba");
                Assert.Equal(@"Symbian^3 Belle", os.Family);
            }
        }
        public class Ubuntu_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Ubuntu_10_04_Line_633() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.2.12) Gecko/20101027 Ubuntu/10.04 (lucid) Firefox/3.6.12");
                Assert.Equal(@"Ubuntu", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"04", os.Minor);
            }

            [Fact]
            public void Ubuntu_10_10_Line_640() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Ubuntu/10.10 Chromium/10.0.648.133 Chrome/10.0.648.133 Safari/534.16");
                Assert.Equal(@"Ubuntu", os.Family);
                Assert.Equal(@"10", os.Major);
                Assert.Equal(@"10", os.Minor);
            }

            [Fact]
            public void Ubuntu_9_04_Line_647() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1.1pre) Gecko/20090717 Ubuntu/9.04 (jaunty) Shiretoko/3.5.1pre");
                Assert.Equal(@"Ubuntu", os.Family);
                Assert.Equal(@"9", os.Major);
                Assert.Equal(@"04", os.Minor);
            }
        }
        public class VRE_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void VRE_Line_654() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (VRE; Opera Mini/4.2/28.2794; U; en) Presto/2.8.119 Version/11.10");
                Assert.Equal(@"VRE", os.Family);
            }
        }
        public class webOS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void webOS_1_2_Line_661() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (webOS/1.2; U; en-US) AppleWebKit/525.27.1 (KHTML, like Gecko) Version/1.0 Safari/525.27.1 Desktop/1.0");
                Assert.Equal(@"webOS", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void webOS_3_0_0_Line_668() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (hp-tablet; Linux; hpwOS/3.0.0; U; en-US) AppleWebKit/534.6 (KHTML, like Gecko) wOSBrowser/233.58 Safari/534.6 TouchPad/1.0");
                Assert.Equal(@"webOS", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"0", os.Patch);
            }

            [Fact]
            public void webOS_3_0_5_Line_675() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (hp-tablet; Linux; hpwOS/3.0.5; U; en-US) AppleWebKit/534.6 (KHTML, like Gecko) wOSBrowser/234.83 Safari/534.6 TouchPad/1.0");
                Assert.Equal(@"webOS", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"5", os.Patch);
            }
        }
        public class WebTV_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void WebTV_2_6_Line_682() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 WebTV/2.6 (compatible; MSIE 4.0)");
                Assert.Equal(@"WebTV", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"6", os.Minor);
            }
        }
        public class WeTab_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void WeTab_Line_689() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; nl-NL) AppleWebKit/534.3 (KHTML, like Gecko) WeTab-Browser Safari/534.3");
                Assert.Equal(@"WeTab", os.Family);
            }
        }
        public class Windows_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_Line_696() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; en-US) AppleWebKit/531.9 (KHTML, like Gecko) AdobeAIR/2.5.1");
                Assert.Equal(@"Windows", os.Family);
            }
        }
        public class Windows_2000_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_2000_Line_703() 
            { 
                var os = Parser.ParseOS(@"ICE Browser/5.05 (Java 1.4.0; Windows 2000 5.0 x86)");
                Assert.Equal(@"Windows 2000", os.Family);
            }
        }
        public class Windows_3_1_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_3_1_Line_710() 
            { 
                var os = Parser.ParseOS(@"NCSA_Mosaic/2.0 (Windows 3.1)");
                Assert.Equal(@"Windows 3.1", os.Family);
            }

            [Fact]
            public void Windows_3_1_Line_1606() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win16; en-US; rv:1.7) Safari/85.5");
                Assert.Equal(@"Windows 3.1", os.Family);
            }

            [Fact]
            public void Windows_3_1_Line_1613() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win3.1; en-US; rv:1.0.0) Gecko/20020530");
                Assert.Equal(@"Windows 3.1", os.Family);
            }

            [Fact]
            public void Windows_3_1_Line_1620() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win3.11; en-US; rv:21.56.11.90) Gecko/20500230 Firefox/3.5");
                Assert.Equal(@"Windows 3.1", os.Family);
            }
        }
        public class Windows_8_1_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_8_1_Line_717() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
                Assert.Equal(@"Windows 8.1", os.Family);
            }
        }
        public class Windows_RT_8_1_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_RT_8_1_Line_724() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows NT 6.3; ARM; WOW64; Trident/7.0; rv:11.0) like Gecko");
                Assert.Equal(@"Windows RT 8.1", os.Family);
            }
        }
        public class Windows_7_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_7_Line_731() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.17) Gecko/20110414 Thunderbird/3.1.10 ThunderBrowse/3.3.5");
                Assert.Equal(@"Windows 7", os.Family);
            }

            [Fact]
            public void Windows_7_Line_738() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.3a1) Gecko/20100208 MozillaDeveloperPreview/3.7a1 (.NET CLR 3.5.30729)");
                Assert.Equal(@"Windows 7", os.Family);
            }

            [Fact]
            public void Windows_7_Line_745() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/534+ (KHTML, like Gecko) FireWeb/1.0.0.0");
                Assert.Equal(@"Windows 7", os.Family);
            }

            [Fact]
            public void Windows_7_Line_752() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 6.1; zh_CN) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0 baidubrowser/1.x Safari/534.7");
                Assert.Equal(@"Windows 7", os.Family);
            }

            [Fact]
            public void Windows_7_Line_759() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; baidubrowser 1.x)");
                Assert.Equal(@"Windows 7", os.Family);
            }
        }
        public class Windows_Phone_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_Phone_Line_766() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; XBLWP7; ZuneWP7)");
                Assert.Equal(@"Windows Phone", os.Family);
            }

            [Fact]
            public void Windows_Phone_7_0_Line_773() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0; SAMSUNG; SGH-i917)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Windows_Phone_7_5_Line_780() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; NOKIA; Lumia 800)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"5", os.Minor);
            }

            [Fact]
            public void Windows_Phone_8_0_Line_787() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 920)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"8", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Windows_Phone_7_10_Line_1452() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (Windows; U; wds 7.10; en-US; DELL; Venue Pro) U2/1.0.0 UCBrowser/3.0.0.285 U2/1.0.0 Mobile");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"10", os.Minor);
            }

            [Fact]
            public void Windows_Phone_8_0_Line_1459() 
            { 
                var os = Parser.ParseOS(@"UCWEB/2.0 (Windows; U; wds 8.0; zh-CN; NOKIA; RM-910_apac_prc_200) U2/1.0.0 UCBrowser/3.0.1.302 U2/1.0.0 Mobile");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"8", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void Windows_Phone_6_5_Line_1529() 
            { 
                var os = Parser.ParseOS(@"acer_S200 Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; Windows Phone 6.5)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"5", os.Minor);
            }

            [Fact]
            public void Windows_Phone_6_5_Line_1550() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; HTC_Touch_Diamond2_T5353; Windows Phone 6.5)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"5", os.Minor);
            }

            [Fact]
            public void Windows_Phone_6_5_Line_1557() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; HTC_Touch2_T3333; Windows Phone 6.5)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"5", os.Minor);
            }

            [Fact]
            public void Windows_Phone_6_5_Line_1564() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; T-Mobile_LEO; Windows Phone 6.5)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"5", os.Minor);
            }

            [Fact]
            public void Windows_Phone_6_5_Line_1571() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; Windows Phone 6.5; SonyEricssonM1i/R1BA; Profile/MIDP-2.1; Configuration/CLDC-1.1; Windows Phone 6.5.3.5)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"5", os.Minor);
            }
        }
        public class Windows_8_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_8_Line_794() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)");
                Assert.Equal(@"Windows 8", os.Family);
            }
        }
        public class Windows_RT_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_RT_Line_801() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; ARM; Trident/6.0)");
                Assert.Equal(@"Windows RT", os.Family);
            }
        }
        public class Windows_Vista_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_Vista_Line_808() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; chromeframe; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729)");
                Assert.Equal(@"Windows Vista", os.Family);
            }
        }
        public class Windows_XP_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_XP_Line_816() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.17) Gecko/20110414 Lightning/1.0b3pre Thunderbird/3.1.10");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_823() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Windows NT 5.1; U; ru) Presto/2.5.24 Version/10.53");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_830() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; BOLT/2.101) AppleWebKit/530  (KHTML, like Gecko) Version/4.0 Safari/530.17");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_837() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; chromeframe; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; Sleipnir 2.8.5)3.0.30729)");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_845() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6; chromeframe; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_853() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6; .NET CLR 2.0.50727; .NET CLR 1.1.4322)");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_861() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.3 (KHTML, like Gecko) RockMelt/0.8.34.841 Chrome/6.0.472.63 Safari/534.3");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_868() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Comodo_Dragon/4.1.1.11 Chrome/4.1.249.1042 Safari/532.5");
                Assert.Equal(@"Windows XP", os.Family);
            }

            [Fact]
            public void Windows_XP_Line_875() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows NT 5.1; rv:2.0) Gecko/20110407 Firefox/4.0.3 PaleMoon/4.0.3");
                Assert.Equal(@"Windows XP", os.Family);
            }
        }
        public class Mandriva_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Mandriva_2008_1_Line_882() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.8.1.13) Gecko/20080208 Mandriva/2.0.0.13-1mdv2008.1 (2008.1) Firefox/2.0.0.13");
                Assert.Equal(@"Mandriva", os.Family);
                Assert.Equal(@"2008", os.Major);
                Assert.Equal(@"1", os.Minor);
            }
        }
        public class Windows_ME_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_ME_Line_889() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win 9x 4.90; PL; rv:1.7.5) Gecko/20041217");
                Assert.Equal(@"Windows ME", os.Family);
            }

            [Fact]
            public void Windows_ME_Line_1592() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win 9x 4.90; de; rv:1.8.1.7) Gecko/20070914 Firefox/2.0.0.7");
                Assert.Equal(@"Windows ME", os.Family);
            }

            [Fact]
            public void Windows_ME_Line_1599() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win 9x 4.90; en-CA; rv:0.9.4.1) Gecko/20020314 Netscape6/6.2.2");
                Assert.Equal(@"Windows ME", os.Family);
            }
        }
        public class LG_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void LG_2013_Line_896() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Unknown; Linux armv7l) AppleWebKit/537.1+ (KHTML, like Gecko) Safari/537.1+ HbbTV/1.1.1 ( ;LGE ;NetCast 4.0 ;03.20.30 ;1.0M ;)");
                Assert.Equal(@"LG", os.Family);
                Assert.Equal(@"2013", os.Major);
            }

            [Fact]
            public void LG_2012_Line_903() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (DirectFB; Linux armv7l) AppleWebKit/534.26+ (KHTML, like Gecko) Version/5.0 Safari/534.26+ HbbTV/1.1.1 ( ;LGE ;NetCast 3.0 ;1.0 ;1.0M ;)");
                Assert.Equal(@"LG", os.Family);
                Assert.Equal(@"2012", os.Major);
            }
        }
        public class Sony_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Sony_2013_Line_910() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Linux armv7l; HbbTV/1.1.1 (; Sony; KDL32W650A; PKG3.211EUA; 2013;); ) Presto/2.12.362 Version/12.11");
                Assert.Equal(@"Sony", os.Family);
                Assert.Equal(@"2013", os.Major);
            }

            [Fact]
            public void Sony_2012_Line_917() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Linux mips; U;  HbbTV/1.1.1 (; Sony; KDL40HX751; PKG1.902EUA; 2012;);; en) Presto/2.10.250 Version/11.60");
                Assert.Equal(@"Sony", os.Family);
                Assert.Equal(@"2012", os.Major);
            }

            [Fact]
            public void Sony_2011_Line_924() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Linux mips; U;  HbbTV/1.1.1 (; Sony; KDL22EX320; PKG4.017EUA; 2011;);; en) Presto/2.7.61 Version/11.00");
                Assert.Equal(@"Sony", os.Family);
                Assert.Equal(@"2011", os.Major);
            }
        }
        public class Samsung_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Samsung_2013_UE40F7000_Line_932() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;Samsung;SmartTV2013;T-FXPDEUC-1102.2;;) WebKit");
                Assert.Equal(@"Samsung", os.Family);
                Assert.Equal(@"2013", os.Major);
                Assert.Equal(@"UE40F7000", os.Minor);
            }

            [Fact]
            public void Samsung_2013_UE32F4500_Line_940() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;Samsung;SmartTV2013;T-MST12DEUC-1102.1;;) WebKit");
                Assert.Equal(@"Samsung", os.Family);
                Assert.Equal(@"2013", os.Major);
                Assert.Equal(@"UE32F4500", os.Minor);
            }

            [Fact]
            public void Samsung_2012_Line_948() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;Samsung;SmartTV2012;;;) WebKit");
                Assert.Equal(@"Samsung", os.Family);
                Assert.Equal(@"2012", os.Major);
            }

            [Fact]
            public void Samsung_2011_Line_955() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;;;;;) Maple_2011");
                Assert.Equal(@"Samsung", os.Family);
                Assert.Equal(@"2011", os.Major);
            }
        }
        public class Philips_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Philips_2012_Line_962() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Linux mips ; U; HbbTV/1.1.1 (; Philips; ; ; ; ) CE-HTML/1.0 NETTV/3.2.1; en) Presto/2.6.33 Version/10.70");
                Assert.Equal(@"Philips", os.Family);
                Assert.Equal(@"2012", os.Major);
            }

            [Fact]
            public void Philips_2013_Line_969() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (Linux mips; U; HbbTV/1.1.1 (; Philips; ; ; ; ) CE-HTML/1.0 NETTV/4.1.3 PHILIPSTV/1.1.1; en) Presto/2.10.250 Version/11.60");
                Assert.Equal(@"Philips", os.Family);
                Assert.Equal(@"2013", os.Major);
            }
        }
        public class Panasonic_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Panasonic_2011_Line_976() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;Panasonic;VIERA 2011;f.532;0071-0802 2000-0000;)");
                Assert.Equal(@"Panasonic", os.Family);
                Assert.Equal(@"2011", os.Major);
            }

            [Fact]
            public void Panasonic_2012_Line_983() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;Panasonic;VIERA 2012;1.261;0071-3103 2000-0000;)");
                Assert.Equal(@"Panasonic", os.Family);
                Assert.Equal(@"2012", os.Major);
            }

            [Fact]
            public void Panasonic_2013_Line_990() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.2.1 (;Panasonic;VIERA 2013;3.672;4101-0003 0002-0000;)");
                Assert.Equal(@"Panasonic", os.Family);
                Assert.Equal(@"2013", os.Major);
            }
        }
        public class FireHbbTV_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void FireHbbTV_1_1_20_Line_997() 
            { 
                var os = Parser.ParseOS(@"HbbTV/1.1.1 (;;;;;) firetv-firefox-plugin 1.1.20");
                Assert.Equal(@"FireHbbTV", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"20", os.Patch);
            }
        }
        public class OpenBSD_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void OpenBSD_3_8_Line_1221() 
            { 
                var os = Parser.ParseOS(@"ELinks/0.10.5 (textmode; OpenBSD 3.8 i386; 131x17-2)");
                Assert.Equal(@"OpenBSD", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"8", os.Minor);
            }

            [Fact]
            public void OpenBSD_3_3_Line_1347() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (OpenBSD 3.3 i386; U) Opera 6.03  [en]");
                Assert.Equal(@"OpenBSD", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"3", os.Minor);
            }
        }
        public class FreeBSD_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void FreeBSD_7_0_Line_1228() 
            { 
                var os = Parser.ParseOS(@"ELinks/0.11.4 (textmode; FreeBSD 7.0-RELEASE-p4 i386; 80x24-2)");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void FreeBSD_4_8_Line_1249() 
            { 
                var os = Parser.ParseOS(@"Links (0.99pre9; FreeBSD 4.8-RELEASE i386; 80x25)");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"8", os.Minor);
            }

            [Fact]
            public void FreeBSD_5_3_Line_1270() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 5.0; FreeBSD 5.3-RC2 i386) Opera 6.12  [en]");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"5", os.Major);
                Assert.Equal(@"3", os.Minor);
            }

            [Fact]
            public void FreeBSD_4_9_Line_1291() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.61 [en] (X11; U; FreeBSD 4.9-RC i386)");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"9", os.Minor);
            }

            [Fact]
            public void FreeBSD_Line_1354() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.19) Gecko/2010031218 FreeBSD/i386 Firefox/3.0.19,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"FreeBSD", os.Family);
            }

            [Fact]
            public void FreeBSD_5_2_1_Line_1368() 
            { 
                var os = Parser.ParseOS(@"NCSA_Mosaic/2.8 (X11; FreeBSD 5.2.1 i686)");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"5", os.Major);
                Assert.Equal(@"2", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }

            [Fact]
            public void FreeBSD_6_1_Line_1382() 
            { 
                var os = Parser.ParseOS(@"Opera/9.51 (X11; FreeBSD 6.1-RELEASE i386; U; en)");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"1", os.Minor);
            }

            [Fact]
            public void FreeBSD_9_0_Line_1389() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (X11; FreeBSD 9.0-CURRENT i386; U; en) Presto/2.9.168 Version/11.50");
                Assert.Equal(@"FreeBSD", os.Family);
                Assert.Equal(@"9", os.Major);
                Assert.Equal(@"0", os.Minor);
            }
        }
        public class Maemo_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Maemo_Line_1284() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 8.0; Linux armv7l; Maemo; Opera Mobi/9; es-ES) Opera 11.00");
                Assert.Equal(@"Maemo", os.Family);
            }
        }
        public class Gentoo_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Gentoo_2_4_20_Line_1312() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; Konqueror/3.2.; Linux 2.4.20-gentoo-r2; X11; i");
                Assert.Equal(@"Gentoo", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"4", os.Minor);
                Assert.Equal(@"20", os.Patch);
            }

            [Fact]
            public void Gentoo_2_4_20_Line_1333() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux 2.4.20-gentoo-r5 i686; U) Opera 7.11  [en]");
                Assert.Equal(@"Gentoo", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"4", os.Minor);
                Assert.Equal(@"20", os.Patch);
            }
        }
        public class NetBSD_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void NetBSD_4_0_Line_1326() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; Konqueror/3.5; NetBSD 4.0_RC3; X11) KHTML/3.5.7 (like Gecko)");
                Assert.Equal(@"NetBSD", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
            }

            [Fact]
            public void NetBSD_1_6_2_Line_1340() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (NetBSD 1.6.2; U) [en]");
                Assert.Equal(@"NetBSD", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"2", os.Patch);
            }
        }
        public class GoogleTV_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void GoogleTV_3_2_Line_1494() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; GoogleTV 3.2; LG Google TV G3 Build/MASTER) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Safari/534.24");
                Assert.Equal(@"GoogleTV", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void GoogleTV_3_2_Line_1501() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; GoogleTV 3.2; NSZ-GS7/GX70 Build/MASTER) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Safari/534.24");
                Assert.Equal(@"GoogleTV", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void GoogleTV_4_0_4_Line_1508() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; GoogleTV 4.0.4; LG Google TV Build/000000) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Safari/534.24");
                Assert.Equal(@"GoogleTV", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }

            [Fact]
            public void GoogleTV_Line_1515() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux armv7l) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Large Screen Safari/534.24 GoogleTV/000000");
                Assert.Equal(@"GoogleTV", os.Family);
            }

            [Fact]
            public void GoogleTV_Line_1522() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.127 Large Screen Safari/533.4 GoogleTV/162853");
                Assert.Equal(@"GoogleTV", os.Family);
            }
        }
        public class Windows_Mobile_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_Mobile_Line_1536() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 7.11) 320x240; VZW; Motorola-A4500; Windows Mobile 6.1 Standard");
                Assert.Equal(@"Windows Mobile", os.Family);
            }

            [Fact]
            public void Windows_Mobile_Line_1543() 
            { 
                var os = Parser.ParseOS(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 7.11) 320x240; VZW; Motorola-Q9c; Windows Mobile 6.1 Standard");
                Assert.Equal(@"Windows Mobile", os.Family);
            }

            [Fact]
            public void Windows_Mobile_Line_1683() 
            { 
                var os = Parser.ParseOS(@"SAMSUNG-GT-B7350/1.0 Opera/9.80 (Windows Mobile; Windows CE; Opera Mobi/ORS-75XXX; U) Presto/2, 4, 13 Version/10.00");
                Assert.Equal(@"Windows Mobile", os.Family);
            }

            [Fact]
            public void Windows_Mobile_Line_1690() 
            { 
                var os = Parser.ParseOS(@"SAMSUNG-GT-B7350/1.0 Opera/9.80 (Windows Mobile; Windows CE; Opera Mobi/ORS-75XXX; U) Presto/2,4,13 Version/10.00");
                Assert.Equal(@"Windows Mobile", os.Family);
            }
        }
        public class Windows_NT_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_NT_Line_1578() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows U; Win NT 5.0; en-US; rv:1.8.0.2) Gecko/20060308 Firefox/1.5.0.2");
                Assert.Equal(@"Windows NT", os.Family);
            }

            [Fact]
            public void Windows_NT_Line_1585() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; N; WinNT; en-US; m14) Netscape6/6.0b1,gzip(gfe),gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Windows NT", os.Family);
            }
        }
        public class Windows_95_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_95_Line_1627() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win32; rv:1.6),gzip(gfe),gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Windows 95", os.Family);
            }

            [Fact]
            public void Windows_95_Line_1634() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win95; en-GB; rv:1.0.1) Gecko/20020823 Netscape/7.0");
                Assert.Equal(@"Windows 95", os.Family);
            }

            [Fact]
            public void Windows_95_Line_1641() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; Win95; en-GB; rv:1.8.0.8) Gecko/20061025 Firefox/1.5.0.8");
                Assert.Equal(@"Windows 95", os.Family);
            }
        }
        public class Windows_CE_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_CE_Line_1648() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Windows; U; WindowsCE 5.2; en-US; rv:1.9.2a2pre) Gecko/20090904 Fennec/1.0a3");
                Assert.Equal(@"Windows CE", os.Family);
            }

            [Fact]
            public void Windows_CE_Line_1655() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (WindowsCE 6.0; rv:2.0.1) Gecko Firefox/5.0.1");
                Assert.Equal(@"Windows CE", os.Family);
            }

            [Fact]
            public void Windows_CE_Line_1662() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (WindowsCE 6.0; rv:2.0.1) Gecko/20100101 Firefox/4.0.1 SeaMonkey/2.1.1");
                Assert.Equal(@"Windows CE", os.Family);
            }

            [Fact]
            public void Windows_CE_Line_1669() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (WindowsCE 6.0; rv:2.1.1) Gecko/ Firefox/5.0.1");
                Assert.Equal(@"Windows CE", os.Family);
            }

            [Fact]
            public void Windows_CE_Line_1676() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (WindowsCE; rv:4.0) Gecko/20120320 Firefox/4.0");
                Assert.Equal(@"Windows CE", os.Family);
            }
        }
        public class Nokia_Series_30_Plus_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Nokia_Series_30_Plus_Line_1697() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Series30Plus; Nokia220/10.03.11; Profile/Series30Plus Configuration/Series30Plus) Gecko/20100401 S40OviBrowser/3.8.1.0.5");
                Assert.Equal(@"Nokia Series 30 Plus", os.Family);
            }
        }
    }

    namespace AdditionalOSTests 
    {
        // Following tests were generated automatically from additional_os_tests.yaml

        public class Android_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Android_1_2_Line_3() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android Donut; de-de; HTC Tattoo 1.52.161.1 Build/Donut) AppleWebKit/528.5+ (KHTML, like Gecko) Version/3.1.2 Mobile Safari/525.20.1");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"2", os.Minor);
            }

            [Fact]
            public void Android_2_1_update1_Line_10() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; U; Android 2.1-update1; en-us; Nexus One Build/ERE27) AppleWebKit/530.17 (KHTML, like Gecko) Version/4.0 Mobile Safari/530.17,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Android", os.Family);
                Assert.Equal(@"2", os.Major);
                Assert.Equal(@"1", os.Minor);
                Assert.Equal(@"update1", os.Patch);
            }
        }
        public class BlackBerry_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void BlackBerry_OS_4_6_0_Line_17() 
            { 
                var os = Parser.ParseOS(@"BlackBerry9000/4.6.0.167 Profile/MIDP-2.0 Configuration/CLDC-1.1 VendorID/102");
                Assert.Equal(@"BlackBerry OS", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"0", os.Patch);
                Assert.Equal(@"167", os.PatchMinor);
            }

            [Fact]
            public void BlackBerry_OS_6_0_0_Line_24() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (BlackBerry; U; BlackBerry 9780; en) AppleWebKit/534.8+ (KHTML, like Gecko) Version/6.0.0.526 Mobile Safari/534.8+,gzip(gfe),gzip(gfe),gzip(gfe)");
                Assert.Equal(@"BlackBerry OS", os.Family);
                Assert.Equal(@"6", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"0", os.Patch);
                Assert.Equal(@"526", os.PatchMinor);
            }
        }
        public class CentOS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void CentOS_Line_31() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; 78; CentOS; US-en) AppleWebKit/527+ (KHTML, like Gecko) Bolt/0.862 Version/3.0 Safari/523.15");
                Assert.Equal(@"CentOS", os.Family);
            }
        }
        public class Chrome_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Chrome_OS_13_587_80_Line_38() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; CrOS i686 13.587.80) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.99 Safari/535.1");
                Assert.Equal(@"Chrome OS", os.Family);
                Assert.Equal(@"13", os.Major);
                Assert.Equal(@"587", os.Minor);
                Assert.Equal(@"80", os.Patch);
            }
        }
        public class Fedora_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Fedora_3_0_4_Line_45() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux x86_64; cs-CZ; rv:1.9.0.4) Gecko/2008111217 Fedora/3.0.4-1.fc9 Firefox/3.0.4");
                Assert.Equal(@"Fedora", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }
        }
        public class GoogleTV_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void GoogleTV_Line_52() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; Linux i686) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Large Screen Safari/534.24 GoogleTV/000000");
                Assert.Equal(@"GoogleTV", os.Family);
            }

            [Fact]
            public void GoogleTV_Line_59() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U: Linux i686; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.127 Large Screen Safari/533.4 GoogleTV/b39389");
                Assert.Equal(@"GoogleTV", os.Family);
            }

            [Fact]
            public void GoogleTV_4_0_4_Line_66() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Linux; GoogleTV 4.0.4; LG Google TV Build/000000) AppleWebKit/534.24 (KHTML, like Gecko) Chrome/11.0.696.77 Safari/534.24");
                Assert.Equal(@"GoogleTV", os.Family);
                Assert.Equal(@"4", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"4", os.Patch);
            }
        }
        public class iOS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void iOS_Line_73() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (iPhone; Opera Mini/5.0.019802/21.572; U; en) Presto/2.5.25 Version/10.54");
                Assert.Equal(@"iOS", os.Family);
            }
        }
        public class Linux_Mint_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Linux_Mint_9_Line_80() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.2.8) Gecko/20100723 Linux Mint/9 (Isadora) Firefox/3.6.8");
                Assert.Equal(@"Linux Mint", os.Family);
                Assert.Equal(@"9", os.Major);
            }
        }
        public class Mac_OS_X_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Mac_OS_X_Line_87() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X Mach-O; en-en; rv:1.9.0.12) Gecko/2009070609 Firefox/3.0.12,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Mac OS X", os.Family);
            }
        }
        public class Mandriva_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Mandriva_2009_1_Line_94() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1.2) Gecko/20090807 Mandriva Linux/1.9.1.2-1.1mud2009.1 (2009.1) Firefox/3.5.2 FirePHP/0.3,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Mandriva", os.Family);
                Assert.Equal(@"2009", os.Major);
                Assert.Equal(@"1", os.Minor);
            }
        }
        public class Other_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Other_Line_101() 
            { 
                var os = Parser.ParseOS(string.Empty);
                Assert.Equal(@"Other", os.Family);
            }
        }
        public class PCLinuxOS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void PCLinuxOS_1_9_2_Line_108() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.2.14) Gecko/20110301 PCLinuxOS/1.9.2.14-1pclos2011 (2011) Firefox/3.6.14,gzip(gfe),gzip(gfe),gzip(gfe)");
                Assert.Equal(@"PCLinuxOS", os.Family);
                Assert.Equal(@"1", os.Major);
                Assert.Equal(@"9", os.Minor);
                Assert.Equal(@"2", os.Patch);
                Assert.Equal(@"14", os.PatchMinor);
            }
        }
        public class Puppy_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Puppy_3_6_8_Line_115() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 ( U; Linux x86_32; en-US; rv:1.0) Gecko/20090723 Puppy/3.6.8-0.1.1 Firefox/3.6.7,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Puppy", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"6", os.Minor);
                Assert.Equal(@"8", os.Patch);
            }
        }
        public class Red_Hat_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Red_Hat_3_0_1_Line_122() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.1) Gecko/2008072310 Red Hat/3.0.1-3.el4 Firefox/3.0.1,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Red Hat", os.Family);
                Assert.Equal(@"3", os.Major);
                Assert.Equal(@"0", os.Minor);
                Assert.Equal(@"1", os.Patch);
            }
        }
        public class Slackware_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Slackware_Line_129() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (X11; Linux x86_64; U; Slackware; lt) Presto/2.8.131 Version/11.11");
                Assert.Equal(@"Slackware", os.Family);
            }
        }
        public class Symbian_OS_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_OS_Line_136() 
            { 
                var os = Parser.ParseOS(@"Opera/9.80 (S60; SymbOS; Opera Mobi/499; U; de) Presto/2.4.18 Version/10.00,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Symbian OS", os.Family);
            }

            [Fact]
            public void Symbian_OS_9_2_Line_143() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (SymbianOS/9.2; U; Series60/3.1 NokiaN95/12.0.013; Profile/MIDP-2.0 Configuration/CLDC-1.1 ) AppleWebKit/413 (KHTML, like Gecko) Safari/413");
                Assert.Equal(@"Symbian OS", os.Family);
                Assert.Equal(@"9", os.Major);
                Assert.Equal(@"2", os.Minor);
            }
        }
        public class Symbian_3_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Symbian_3_Line_150() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (Symbian/3; Series60/5.2 NokiaN8-00/010.022; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/525 (KHTML, like Gecko) Version/3.0 BrowserNG/7.2.6.3 3gpp-gba,gzip(gfe),gzip(gfe)");
                Assert.Equal(@"Symbian^3", os.Family);
            }
        }
        public class Windows_Phone_Tests
        {
            static readonly Parser Parser = Parser.GetDefault();

            [Fact]
            public void Windows_Phone_7_5_Line_157() 
            { 
                var os = Parser.ParseOS(@"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; SAMSUNG; SGH-i917)");
                Assert.Equal(@"Windows Phone", os.Family);
                Assert.Equal(@"7", os.Major);
                Assert.Equal(@"5", os.Minor);
            }
        }
    }
}

