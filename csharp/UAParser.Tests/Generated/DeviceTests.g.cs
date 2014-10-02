// The file was automatically generated using a T4 Text Template.
//
// Source   : DeviceTests.g.tt
// Generated: Wed, 28 May 2014 16:22:13 GMT
// Generator: Microsoft.VisualStudio.TextTemplating.VSHost.12.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a

namespace UAParser.Tests.Generated
{
    using UAParser;
    using Xunit;
    
    // ReSharper disable InconsistentNaming

    // Following tests were generated automatically from test_device.yaml

    public class DeviceTests
    {
        static readonly Parser Parser = Parser.GetDefault();

        // ReSharper disable InconsistentNaming

        [Fact]
        public void Alcatel_OT510A_Line_3() 
        { 
            var device = Parser.ParseDevice(@"ALCATEL-OT510A/382 ObigoInternetBrowser/Q05A");
            Assert.Equal(@"Alcatel OT510A", device.Family);
        }

        [Fact]
        public void Alcatel_OH5_Line_6() 
        { 
            var device = Parser.ParseDevice(@"Alcatel-OH5/1.0 UP.Browser/6.1.0.7.7 (GUI) MMP/1.0");
            Assert.Equal(@"Alcatel OH5", device.Family);
        }

        [Fact]
        public void Amaze_4G_Line_9() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; Amaze_4G Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
            Assert.Equal(@"Amaze_4G", device.Family);
        }

        [Fact]
        public void BlackBerry_Line_12() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (BlackBerry; Opera Mini/7.0.31437/28.3030; U; en) Presto/2.8.119 Version/11.10");
            Assert.Equal(@"BlackBerry", device.Family);
        }

        [Fact]
        public void BlackBerry_9320_Line_15() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (BlackBerry; U; BlackBerry 9320; en-GB) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.398 Mobile Safari/534.11+");
            Assert.Equal(@"BlackBerry 9320", device.Family);
        }

        [Fact]
        public void BlackBerry_Playbook_Line_18() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (PlayBook; U; RIM Tablet OS 1.0.0; en-US) AppleWebKit/534.8+ (KHTML, like Gecko) Version/0.0.1 Safari/534.8+");
            Assert.Equal(@"BlackBerry Playbook", device.Family);
        }

        [Fact]
        public void BlackBerry_Touch_Line_21() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (BB10; Touch) AppleWebKit/537.3+ (KHTML, like Gecko) Version/10.0.9.388 Mobile Safari/537.3+");
            Assert.Equal(@"BlackBerry Touch", device.Family);
        }

        [Fact]
        public void Galaxy_Nexus_Line_24() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; Android 4.2; Galaxy Nexus Build/JOP40C) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19");
            Assert.Equal(@"Galaxy Nexus", device.Family);
        }

        [Fact]
        public void GT_P7510_Line_27() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 3.0.1; en-us; GT-P7510 Build/HRI83) AppleWebKit/534.13 (KHTML, like Gecko) Version/4.0 Safari/534.13");
            Assert.Equal(@"GT-P7510", device.Family);
        }

        [Fact]
        public void HP_TouchPad_Line_30() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (hp-tablet; Linux; hpwOS/3.0.5; U; en-US) AppleWebKit/534.6 (KHTML, like Gecko) wOSBrowser/234.83 Safari/534.6 TouchPad/1.0");
            Assert.Equal(@"HP TouchPad", device.Family);
        }

        [Fact]
        public void HTC_Desire_Line_33() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 2.2.2; en-gb; HTC Desire Build/FRG83G) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
            Assert.Equal(@"HTC Desire", device.Family);
        }

        [Fact]
        public void HTC_WildfireS_Line_36() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 2.3.3; en-fr; HTC/WildfireS/1.33.163.2 Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
            Assert.Equal(@"HTC WildfireS", device.Family);
        }

        [Fact]
        public void HTC_Hero_Line_39() 
        { 
            var device = Parser.ParseDevice(@"QQBrowser (Linux; U; zh-cn; HTC Hero Build/FRF91)");
            Assert.Equal(@"HTC Hero", device.Family);
        }

        [Fact]
        public void Huawei_G2800_Line_42() 
        { 
            var device = Parser.ParseDevice(@"Huawei/1.0/0HuaweiG2800/WAP2.0/Obigo-Browser/Q03C MMS/Obigo-MMS/1.2");
            Assert.Equal(@"Huawei G2800", device.Family);
        }

        [Fact]
        public void Huawei_M750_Line_45() 
        { 
            var device = Parser.ParseDevice(@"HUAWEI-M750/001.00 ACS-NetFront/3.2");
            Assert.Equal(@"Huawei M750", device.Family);
        }

        [Fact]
        public void iPad_Line_48() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B367 Safari/531.21.10");
            Assert.Equal(@"iPad", device.Family);
        }

        [Fact]
        public void iPhone_Line_51() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPhone; U; fr; CPU iPhone OS 4_2_1 like Mac OS X; fr) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8C148a Safari/6533.18.5");
            Assert.Equal(@"iPhone", device.Family);
        }

        [Fact]
        public void iPod_Line_54() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPod; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5");
            Assert.Equal(@"iPod", device.Family);
        }

        [Fact]
        public void iPod_Line_57() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPod touch; CPU iPhone OS 7_0 like Mac OS X) AppleWebKit/537.40 (KHTML, like Gecko) Version/6.0 Mobile/11A4400f Safari/8536.25");
            Assert.Equal(@"iPod", device.Family);
        }

        [Fact]
        public void Kindle_Line_60() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (compatible; Linux 2.6.10) NetFront/3.3 Kindle/1.0 (screen 600x800)");
            Assert.Equal(@"Kindle", device.Family);
        }

        [Fact]
        public void Kindle_Fire_Line_63() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 2.3.4; en-us; Kindle Fire Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
            Assert.Equal(@"Kindle Fire", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HD_Line_66() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFTT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.0 Safari/535.19 Silk-Accelerated=false");
            Assert.Equal(@"Kindle Fire HD", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HD_Line_69() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; KFTT Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
            Assert.Equal(@"Kindle Fire HD", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HD_Line_72() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFTT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.2 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HD", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HD_7_WiFi_Line_75() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFSOWI Build/JDQ39) AppleWebKit/535.19 (KHTML, like Gecko) Silk/3.7 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HD 7"" WiFi", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HDX_7_WiFi_Line_78() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFTHWI Build/JDQ39) AppleWebKit/535.19 (KHTML, like Gecko) Silk/3.6 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HDX 7"" WiFi", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HDX_7_4G_Line_81() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFTHWA Build/JDQ39) AppleWebKit/535.19 (KHTML, like Gecko) Silk/3.6 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HDX 7"" 4G", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HDX_8_9_WiFi_Line_84() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFAPWI Build/JDQ39) AppleWebKit/535.19 (KHTML, like Gecko) Silk/3.6 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HDX 8.9"" WiFi", device.Family);
        }

        [Fact]
        public void Kindle_Fire_HDX_8_9_4G_Line_87() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFAPWA Build/JDQ39) AppleWebKit/535.19 (KHTML, like Gecko) Silk/3.6 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire HDX 8.9"" 4G", device.Family);
        }

        [Fact]
        public void Kindle_Fire_Line_90() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-us; Silk/1.1.0-80) AppleWebKit/533.16 (KHTML, like Gecko) Version/5.0 Safari/533.16 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire", device.Family);
        }

        [Fact]
        public void Kindle_Fire_Line_93() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-us; KFOT Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Silk/2.1 Safari/535.19 Silk-Accelerated=true");
            Assert.Equal(@"Kindle Fire", device.Family);
        }

        [Fact]
        public void Kindle_Fire_Line_96() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 4.0.3; en-us; KFOT Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
            Assert.Equal(@"Kindle Fire", device.Family);
        }

        [Fact]
        public void Kindle_Line_99() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; en-US) AppleWebKit/528.5+ (KHTML, like Gecko, Safari/528.5+) Version/4.0 Kindle/3.0 (screen 600x800; rotate)");
            Assert.Equal(@"Kindle", device.Family);
        }

        [Fact]
        public void LG_272_Line_102() 
        { 
            var device = Parser.ParseDevice(@"NetFront/4.2 (BMP 1.0.4; U; en-us; LG; NetFront/4.2/AMB) Boost LG272 MMP/2.0 Profile/MIDP-2.1 Configuration/CLDC-1.1");
            Assert.Equal(@"LG 272", device.Family);
        }

        [Fact]
        public void LG_VN271_Line_105() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (BREW; Opera Mini/5.1.191/27.2202; U; en) Presto/2.8.119 240X400 LG VN271");
            Assert.Equal(@"LG VN271", device.Family);
        }

        [Fact]
        public void LG_T500_Line_108() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (LG-T500 AppleWebkit/531 Browser/Phantom/V2.0 Widget/LGMW/3.0 MMS/LG-MMS-V1.0/1.2 Java/ASVM/1.1 Profile/MIDP-2.1 Configuration/CLDC-1.1)");
            Assert.Equal(@"LG T500", device.Family);
        }

        [Fact]
        public void Lumia_800_Line_111() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; NOKIA; Lumia 800)");
            Assert.Equal(@"Lumia 800", device.Family);
        }

        [Fact]
        public void Lumia_920_Line_114() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 920)");
            Assert.Equal(@"Lumia 920", device.Family);
        }

        [Fact]
        public void Nintendo_DS_Line_117() 
        { 
            var device = Parser.ParseDevice(@"Bunjalloo/0.7.6(Nintendo DS;U;en)");
            Assert.Equal(@"Nintendo DS", device.Family);
        }

        [Fact]
        public void Nintendo_DSi_Line_120() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.50 (Nintendo DSi; Opera/507; U; en-US)");
            Assert.Equal(@"Nintendo DSi", device.Family);
        }

        [Fact]
        public void Nintendo_3DS_Line_123() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Nintendo 3DS; U; ; en) Version/1.7498.US");
            Assert.Equal(@"Nintendo 3DS", device.Family);
        }

        [Fact]
        public void Nintendo_Wii_Line_126() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.30 (Nintendo Wii; U; ; 3642; en)");
            Assert.Equal(@"Nintendo Wii", device.Family);
        }

        [Fact]
        public void Nintendo_Wii_U_Line_129() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Nintendo WiiU) AppleWebKit/534.52 (KHTML, like Gecko) NX/2.1.0.8.21 NintendoBrowser/1.0.0.7494.US");
            Assert.Equal(@"Nintendo Wii U", device.Family);
        }

        [Fact]
        public void Nokia_201_Line_132() 
        { 
            var device = Parser.ParseDevice(@"Nokia201/2.0 (11.21) Profile/MIDP-2.1 Configuration/CLDC-1.1 Mozilla/5.0 (Java; U; en-us; nokia201) UCBrowser8.3.0.154/70/355/UCWEB Mobile");
            Assert.Equal(@"Nokia 201", device.Family);
        }

        [Fact]
        public void Nokia_5130c_2_Line_135() 
        { 
            var device = Parser.ParseDevice(@"iBrowser/Mini2.8 (Nokia5130c-2/07.97)");
            Assert.Equal(@"Nokia 5130c-2", device.Family);
        }

        [Fact]
        public void Nokia_5320di_Line_138() 
        { 
            var device = Parser.ParseDevice(@"Nokia5320di/UCWEB8.0.3.99/28/999");
            Assert.Equal(@"Nokia 5320di", device.Family);
        }

        [Fact]
        public void Nokia_5800d_1_Line_141() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (SymbianOS/9.4; U; Series60/5.0 Nokia5800d-1/21.0.025; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/413 (KHTML, like Gecko) Safari/413");
            Assert.Equal(@"Nokia 5800d-1", device.Family);
        }

        [Fact]
        public void Nokia_6120c_Line_144() 
        { 
            var device = Parser.ParseDevice(@"NOKIA6120c/UC Browser7.4.0.65/28/352");
            Assert.Equal(@"Nokia 6120c", device.Family);
        }

        [Fact]
        public void Nokia_701_Line_147() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Symbian/3; Series60/5.3 Nokia701/111.020.0307; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/533.4 (KHTML, like Gecko) NokiaBrowser/7.4.1.14 Mobile Safari/533.4 3gpp-gba");
            Assert.Equal(@"Nokia 701", device.Family);
        }

        [Fact]
        public void Nokia_C2_00_Line_150() 
        { 
            var device = Parser.ParseDevice(@"OneBrowser/3.0 (NokiaC2-00/03.42)");
            Assert.Equal(@"Nokia C2-00", device.Family);
        }

        [Fact]
        public void Nokia_C2_03_Line_153() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Series40; NokiaC2-03/07.48; Profile/MIDP-2.1 Configuration/CLDC-1.1) Gecko/20100401 S40OviBrowser/2.2.0.0.33");
            Assert.Equal(@"Nokia C2-03", device.Family);
        }

        [Fact]
        public void Nokia_C5_03_Line_156() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (S60V5; U; en-us; NokiaC5-03) AppleWebKit/530.13 (KHTML, like Gecko) UCBrowser/8.7.0.218/50/352/UCWEB Mobile");
            Assert.Equal(@"Nokia C5-03", device.Family);
        }

        [Fact]
        public void Nokia_X2_05_Line_159() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Series40; NokiaX2-05/08.35; Profile/MIDP-2.1 Configuration/CLDC-1.1) Gecko/20100401 S40OviBrowser/2.0.2.68.14");
            Assert.Equal(@"Nokia X2-05", device.Family);
        }

        [Fact]
        public void Nokia_N8_Line_162() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Symbian/3; Series60/5.2 NokiaN8-00/013.016; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/525 (KHTML, like Gecko) Version/3.0 BrowserNG/7.2.8.10 3gpp-gba");
            Assert.Equal(@"Nokia N8", device.Family);
        }

        [Fact]
        public void Nokia_N8_Line_165() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Symbian/3; Series60/5.2 NokiaN8-00/012.002; Profile/MIDP-2.1 Configuration/CLDC-1.1 ) AppleWebKit/533.4 (KHTML, like Gecko) NokiaBrowser/7.3.0 Mobile Safari/533.4 3gpp-gba");
            Assert.Equal(@"Nokia N8", device.Family);
        }

        [Fact]
        public void Nokia_N9_Line_168() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
            Assert.Equal(@"Nokia N9", device.Family);
        }

        [Fact]
        public void Other_Line_171() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; XBLWP7; ZuneWP7)");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_174() 
        { 
            var device = Parser.ParseDevice(@"IUC(U;iOS 5.1.1;Zh-cn;320*480;)/UCWEB7.9.0.94/41/997");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_177() 
        { 
            var device = Parser.ParseDevice(@"J2ME/UCWEB7.0.3.45/139/7682");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_180() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (BREW 3.1.5; U; en-us; Sanyo; NetFront/3.5.1/AMB) Boost SCP3810");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_183() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (Brew MP 1.0.2; U; en-us; Sanyo; NetFront/3.5.1/AMB) Sprint E4100");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_186() 
        { 
            var device = Parser.ParseDevice(@"NetFront/3.5.1 (BREW 3.1.5; U; en-us; LG; NetFront/3.5.1/WAP) Sprint LN240 MMP/2.0 Profile/MIDP-2.1 Configuration/CLDC-1.1");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_189() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (compatible; MSIE 5.17; Mac_PowerPC)");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_192() 
        { 
            var device = Parser.ParseDevice(@"NCSA_Mosaic/2.0 (Windows 3.1)");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_195() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (X11; U; SunOS i86pc; en-US; rv:1.8.0.5) Gecko/20060728 Firefox/1.5.0.5");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_198() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Windows; U; Windows NT 6.1; zh_CN) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0 baidubrowser/1.x Safari/534.7");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_201() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; baidubrowser 1.x)");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_204() 
        { 
            var device = Parser.ParseDevice(@"ICE Browser/5.05 (Java 1.4.0; Windows 2000 5.0 x86)");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_207() 
        { 
            var device = Parser.ParseDevice(@"MQQBrowser/371 Mozilla/5.0 (iPhone 4S; CPU iPhone OS 6_0_1 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Mobile/10A523 Safari/7534.48.3");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_210() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (VRE; Opera Mini/4.2/28.2794; U; en) Presto/2.8.119 Version/11.10");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_213() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Mobile; rv:15.0) Gecko/15.0 Firefox/15.0");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Other_Line_216() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Tablet; rv:29.0) Gecko/29.0 Firefox/29.0");
            Assert.Equal(@"Other", device.Family);
        }

        [Fact]
        public void Pantech_P6010_Line_219() 
        { 
            var device = Parser.ParseDevice(@"PantechP6010/JNUS11072011 BMP/1.0.2 DeviceId/141020 NetFront/4.1 OMC/1.5.3 Profile/MIDP-2.1 Configuration/CLDC-1.1");
            Assert.Equal(@"Pantech P6010", device.Family);
        }

        [Fact]
        public void Pantech_P7040_Line_222() 
        { 
            var device = Parser.ParseDevice(@"PantechP7040/JLUS04042011 Browser/Obigo/Q05A OMC/1.5.3 Profile/MIDP-2.1 Configuration/CLDC-1.1");
            Assert.Equal(@"Pantech P7040", device.Family);
        }

        [Fact]
        public void PJ83100_2_20_502_7_Line_225() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 4.0.4; en-us; PJ83100/2.20.502.7 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.0");
            Assert.Equal(@"PJ83100/2.20.502.7", device.Family);
        }

        [Fact]
        public void PlayStation_3_Line_228() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (PLAYSTATION 3; 3.55)");
            Assert.Equal(@"PlayStation 3", device.Family);
        }

        [Fact]
        public void PlayStation_3_Line_231() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (PLAYSTATION 3 4.31) AppleWebKit/531.22.8 (KHTML, like Gecko)");
            Assert.Equal(@"PlayStation 3", device.Family);
        }

        [Fact]
        public void PlayStation_Portable_Line_234() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 (PSP (PlayStation Portable); 2.00)");
            Assert.Equal(@"PlayStation Portable", device.Family);
        }

        [Fact]
        public void PlayStation_Vita_Line_237() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (PlayStation Vita 1.81) AppleWebKit/531.22.8 (KHTML, like Gecko) Silk/3.2");
            Assert.Equal(@"PlayStation Vita", device.Family);
        }

        [Fact]
        public void Samsung_C3053_Line_240() 
        { 
            var device = Parser.ParseDevice(@"SAMSUNG-C3053/1.0 Openwave/6.2.3 Profile/MIDP-2.0 Configuration/CLDC-1.1 UP.Browser/6.2.3.3.c.1.101 (GUI) MMP/2.0");
            Assert.Equal(@"Samsung C3053", device.Family);
        }

        [Fact]
        public void Samsung_GT_S5253_Line_243() 
        { 
            var device = Parser.ParseDevice(@"OneBrowser/3.0 (SAMSUNG-GT-S5253/S5253DDKJ2)");
            Assert.Equal(@"Samsung GT-S5253", device.Family);
        }

        [Fact]
        public void Sega_Dreamcast_Line_246() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/3.0 (Planetweb/2.100 JS SSL US; Dreamcast US)");
            Assert.Equal(@"Sega Dreamcast", device.Family);
        }

        [Fact]
        public void SPH_L710_Line_249() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; Android 4.1.1; SPH-L710 Build/JRO03L) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19");
            Assert.Equal(@"SPH-L710", device.Family);
        }

        [Fact]
        public void Spider_Line_252() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void TECNO_T3_Line_255() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux;U;Android 2.3.5;en-us;TECNO T3 Build/master) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");
            Assert.Equal(@"TECNO T3", device.Family);
        }

        [Fact]
        public void Tesla_Model_S_Line_258() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (X11i; Linux; C) AppleWebKikt/533.3 (KHTML, like Gecko) QtCarBrowser Safari/533.3");
            Assert.Equal(@"Tesla Model S", device.Family);
        }

        [Fact]
        public void WebTV_Line_261() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/4.0 WebTV/2.6 (compatible; MSIE 4.0)");
            Assert.Equal(@"WebTV", device.Family);
        }

        [Fact]
        public void Spider_Line_264() 
        { 
            var device = Parser.ParseDevice(@"AdsBot-Google-Mobile (+http://www.google.com/mobile/adsbot.html) Mozilla (iPhone; U; CPU iPhone OS 3 0 like Mac OS X) AppleWebKit (KHTML, like Gecko) Mobile Safari");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_267() 
        { 
            var device = Parser.ParseDevice(@"magpie-crawler/1.1 (U; Linux amd64; en-GB; +http://www.brandwatch.net)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_270() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_1 like Mac OS X; en-us) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8B117 Safari/6531.22.7 (compatible; Googlebot-Mobile/2.1; +http://www.google.com/bot.html)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_273() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void GT_N7000_Line_276() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Linux; U; Android 4.1.2; en-; GT-N7000 Build/JZO54K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
            Assert.Equal(@"GT-N7000", device.Family);
        }

        [Fact]
        public void HbbTV_Line_279() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Panasonic;VIERA 2012;1.261;0071-3103 2000-0000;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_282() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Unknown; Linux armv7l) AppleWebKit/537.1+ (KHTML, like Gecko) Safari/537.1+ HbbTV/1.1.1 ( ;LGE ;NetCast 4.0 ;03.20.30 ;1.0M ;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_285() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (DirectFB; Linux armv7l) AppleWebKit/534.26+ (KHTML, like Gecko) Version/5.0 Safari/534.26+ HbbTV/1.1.1 ( ;LGE ;NetCast 3.0 ;1.0 ;1.0M ;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_288() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (Linux armv7l; HbbTV/1.1.1 (; Sony; KDL32W650A; PKG3.211EUA; 2013;); ) Presto/2.12.362 Version/12.11");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_291() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Panasonic;VIERA 2012;1.261;0071-3103 2000-0000;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_294() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (Unknown; Linux armv7l) AppleWebKit/537.1+ (KHTML, like Gecko) Safari/537.1+ HbbTV/1.1.1 ( ;LGE ;NetCast 4.0 ;03.20.30 ;1.0M ;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_297() 
        { 
            var device = Parser.ParseDevice(@"Mozilla/5.0 (DirectFB; Linux armv7l) AppleWebKit/534.26+ (KHTML, like Gecko) Version/5.0 Safari/534.26+ HbbTV/1.1.1 ( ;LGE ;NetCast 3.0 ;1.0 ;1.0M ;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_300() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (Linux armv7l; HbbTV/1.1.1 (; Sony; KDL32W650A; PKG3.211EUA; 2013;); ) Presto/2.12.362 Version/12.11");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_304() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Samsung;SmartTV2013;T-FXPDEUC-1102.2;;) WebKit");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_308() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Samsung;SmartTV2013;T-MST12DEUC-1102.1;;) WebKit");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_311() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.2.1 (;Panasonic;VIERA 2013;3.672;4101-0003 0002-0000;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_315() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Samsung;SmartTV2012;;;) WebKit");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_318() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Panasonic;VIERA 2012;1.261;0071-3103 2000-0000;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_321() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (Linux mips ; U; HbbTV/1.1.1 (; Philips; ; ; ; ) CE-HTML/1.0 NETTV/3.2.1; en) Presto/2.6.33 Version/10.70");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_324() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;;;;;) Maple_2011");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_327() 
        { 
            var device = Parser.ParseDevice(@"Opera/9.80 (Linux mips; U;  HbbTV/1.1.1 (; Sony; KDL22EX320; PKG4.017EUA; 2011;);; en) Presto/2.7.61 Version/11.00");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_330() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;Panasonic;VIERA 2011;f.532;0071-0802 2000-0000;)");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void HbbTV_Line_333() 
        { 
            var device = Parser.ParseDevice(@"HbbTV/1.1.1 (;;;;;) firetv-firefox-plugin 1.1.20");
            Assert.Equal(@"HbbTV", device.Family);
        }

        [Fact]
        public void Spider_Line_336() 
        { 
            var device = Parser.ParseDevice(@"Netvibes (http://www.netvibes.com)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_339() 
        { 
            var device = Parser.ParseDevice(@"Sogou Pic Spider/3.0(+http://www.sogou.com/docs/help/webmasters.htm#07)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_342() 
        { 
            var device = Parser.ParseDevice(@"ICC-Crawler/2.0 (Mozilla-compatible; ; http://kc.nict.go.jp/project1/crawl.html; zurukko1552995316;)");
            Assert.Equal(@"Spider", device.Family);
        }

        [Fact]
        public void Spider_Line_345() 
        { 
            var device = Parser.ParseDevice(@"Innovazion Crawler/Nutch-1.7");
            Assert.Equal(@"Spider", device.Family);
        }
    }
}

