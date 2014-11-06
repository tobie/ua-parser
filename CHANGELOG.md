# Changelog

**2014-11-05**

- *regexes*
  - Better detection of bots

**2014-10-30**

- *regexes*
  - Puffin browser added
  - Better detection of bots

**2014-09-25**

- *regexes*
  - Zopo devices
  - Asus Transformer, PadFone

**2014-09-24**

- regexes
  - Oppo Find7a
  - ZTE OpenC
  - Fix for Archos/ Arnova Tablets

- 2014-09-02
  - *runtest.sh*
    - script to run tests for all supported parsers
  - Merging changes from `tobie/master`
  - *regexes.yaml*
    - New Spider regex definitions
    - Refined descriptions of CFNetwork versions of iOS. Added PlayStation 4 UA
    - New entries for Spiders, Espial Smart TV, plus fixes for Windows Phones with Android in the UA string
    - Amiga Voyager is not a robot

- 2014-07-20
  - *regexes.yaml*
    - Better detection of Assistant, Thl. Detection of Dex, Enot
    - Better detection of Opera Mobi, Chimera, Bon Echo (Credits to keshonok)

- 2014-06-04
  - *regexes.yaml*
    - Better detection of Alcatel Fire E, Huawei Y.., Acer Liquid Z150, Cubot Bobby, Odys Bravio

- 2014-06-03
  - *regexes.yaml*
    - Better detection of Sony and Wiko Devices

- 2014-04-23
  - *regexes.yaml*
    - Detection of CFNetwork and iOS Applications

- 2014-04-22
  - *regexes.yaml*
    - Better detection of Impression ImPad
    - New Brand Explay
    - Acer S510

- 2014-04-15
  - *java parser*
    - Merging PR #320 from nielsbasjes; multiReplace refactored
    - mvn test pass

- 2014-04-14
  - *regexes.yaml*
    - Detection of Nokia Series 30Plus added
    - Detection of Dolphin Browser (Android + iOs)

- 2014-03-31
  - *regexes.yaml*
    - Simplify GoogleTV
    - Refactor Linux Distros and Versions

- 2014-03-30
  - *regexes.yaml*
    - Merge #353 (QQBrowsers) - Simplified regexes
    - MQQBrowser/Mini added

- 2014-03-05
	- *regexes.yaml*
    - Merge #345 (GooglePlusBot)
	  - Adding Google+ Bot to device_parsers
	- Merge PR #335

- 2014-03-01
  - *regexes.yaml* "user_agent_parsers"
    - improvements: Opera Mobile, Opera Mini, iBrowser, iRAPP Browsers
    - change: Symbian OSS and WAP Browser, SymbianOS
  - *regexes.yaml* "device_parsers"
    - new: Android Application; Operator specific devices Sprint, Cellular split
    - improvements: 3Q spacings, Assistant, Cube, Kyocera, Prestigio, Samsung, Sony C\d{4} separated from Kyocera, Windows Phone added to Generic Smartphone
    - change: HTC device parser refactored
  - *test_device.yaml*
    - url-encoded user-agent strings decoded
  - *pgts_browser_list.yaml*
    - Symbian Browser testcases adjusted

- 2014-02-28
  - *js parser*
    - allow async loading

- 2014-02-20
  - *js parser*
    - new api to allow loading of custom regexes files

- 2014-01-30
  - *regexes.yaml*
    - improvements
      - distinguish between mobile spiders per smart- or feature phone

- 2014-01-29
  - *regexes.yaml*
    - improvements
      - UAs with doubled Samsung Values get corrected
      - devices matching brand SonyEricsson ar shifted to Sony for newer Xperia devices
      - MOZILLA FIREBIRD is not from brand Bird
    - new
      - Nokia Android matchers
    - merge
      - PR#331 Firefox OS for Tablet
      - PHP changes

- 2014-01-18
  - *regexes.yaml*
    - improvements
      - os: Windows XP hides Windows Phone 6; Windows Phone OS/; Opera Mini Bada; WindowsCE not detected for Fennec;
      - device: Haier[ _\-]; new HTC Desire models; IconBit NT; Mobistel Cynus; Mobiistar is not a POV brand; Generic_Android matcher; Nokia UCBrowser; Nokia XpressMusic strip SN number; Samsung- matcher;
      - strip trailing spaces
    - new
      - device: Sony SmartWatch; WeTab;

- 2014-01-14
  - *regexes.yaml*
    - Merge pull request #325 from epgrubmair/mail_clients_1
      Airmail,Outlook,Thunderbird new. Lightning updated

- 2014-01-11
  - *regexes.yaml*
    - improvements
      - Advent Vega, Alcatel One Touch with ",", Blue Tank 4.5, Medion Lifetab, Pomp, Samsung Galaxy S*, ZTE RACERII fix: CobyKyros MID
    - new
      - models: Alcatel One Touch, GoClever QUANTUM, Haier
      - brands: Gfive, Lexibook, Omega

- 2014-01-08
  - *regexes.yaml*
    - new
      - Google Glass added

- 2014-01-07
  - *regexes.yaml*
    - improvements
      - better android matchers if "Build" occurs twice; Generic Smart Phone matcher
    - refactored
      - LG matchers; Generic Android Matchers now resolves brand to "Generic_Android"
    - new
      - models: Acer, Alcatel, Aoson, Arnova, Asus, Blaupunkt, Blu, Casio, Celkon, Cloudfone, Cmx, HTC, Hyundai, Intex, Karbonn, Lenovo, MSI, Nook, Odys, POV, Samsung, Softbank, Terra, Thalia, Thl, Toshiba, Treq, Wiko, Woxter, Yarvik, Xoro, Zopo, ZTE, Hero, HTC Windows Phones, Internet TV Sets, Generic_Inettv, microsoft, Android on Blackberry
      - brand: Airis, Axioo, Cubot, Denver, DNS AirTab, DOOV, Evercoss, Gionee, HCLme, hitech, iBall, IMO, Infinix, Informer, Jaytech, JXD, Kingcom, Lava, Lemon, Mito, Modecom, Multilaser, MyPhone, Papayre, Pipo, Ployer, Pomp, Skytex, Tooky, Videocon, vivo, Walton, Google TV, Generic Tablet
    - changes
      - brand renamed: Hanns is now Hannspree
      - brand removed: Technicolor: doubled - now part of Telstra

- 2013-11-22
  - *regexes.yaml*
    - user_agent_parsers:
      - Bingbot PR#311 added
    - device_parsers:
      - Bingbot PR#311 added (detection was already present; added for completness)
      - More Android brands and models
      - Improvements to previous device detection for some devices

- 2013-11-15
  - *php*
    - Modernize PHP port of User Agent Parser from @lstrojny
    - brand model device parsing with case insensitive testing

- 2013-11-14
  - *regexes.yaml*
    - user_agent_parsers:
      - Vodafone browser removed and corrected to Openwave
    - os_parsers:
      - Android matchers simplified
      - os detection within UCWEB and JUC browsers
      - Windows ME matcher case corrected
      - Windows 8.1 added and changed to 8.1 PR#292
      - Firefox OS Matcher rewritten
      - Mozilla WinXX matchers added
      - Improved detection of iOS
      - Match of GoogleTV patch version
      - Detection of gentoo and Linux Kernel Version
    - device_parsers:
      - Complete rewrite of Brand Model matchers for Android and Windows Phones
      - Kindle Fire HDX added PR#303
      - Detection of Kindle Reader
      - Brand Model recognized for Sony PalmOS devices
      - FireFoxOS devices added (Alcatel One Touch 4012X, ZTE Open)
      - Detection of Siemens and SonyEricsson Feature Phones
      - Detection of Generic Tablets
      - Spider detection for GoogleBots changed to match before Android and iOS
 - *Testcases*
  - *test_user_agent_parser.yaml*
    - New tests added to cover regex changes
    - Testcases corrected
  - *test_user_agent_parser_os.yaml*
    - New tests added to cover regex changes
    - Testcases corrected
  - *test_device.yaml*
    - New tests added to cover the rewrite
    - Testcases corrected, doubled testcases with same user-agent string removed
    - Corrupted User-Agents added in previous PR removed
  - *py*
    - Python Parser updated according to spec. $1 replacement in OSParser for os.family considered.
  - *perl*
    - Perl Parser updated; Speed optimizations using precompiled regexes;
