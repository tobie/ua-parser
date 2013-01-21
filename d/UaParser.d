module UaParser;

import yaml;

private int parseInt(string input) {
    return std.conv.parse!int(input);
}

private class DeviceStore {
    private string rep;
    private string rexp;

    void setRegExp(string r) {
        rexp = r;
    }

    void setRep(string f) {
        rep = f;
    }

    const string getRep() {
        return rep;
    }

    const string getRegExp() {
        return rexp;
    }
}

private class AgentStore : DeviceStore {
    private string majorVersionRep;
    private string minorVersionRep;

    void setMajorVersionRep(string m) {
        majorVersionRep = m;
    }

    void setMinorVersionRep(string m) {
        minorVersionRep = m;
    }

    const string getMajorVersionRep() {
        return majorVersionRep;
    }

    const string getMinorVersionRep() {
        return minorVersionRep;
    }
}

private alias AgentStore OsStore;
private alias AgentStore BrowserStore;

private class Device {
    public string family;

    override const string toString() {
        return family;
    }
}

private class Agent : Device {
    public int major;
    public int minor;
    public int patch;

    override const string toString() {
        return family ~ " " ~ toVersionString();
    }

    const string toVersionString() {
        return std.conv.to!string(major) ~ "." 
             ~ std.conv.to!string(minor) ~ "."
             ~ std.conv.to!string(patch);
    }
}

public alias Agent Os;
public alias Agent Browser;

public class UserAgent {
    public Device device;

    public Os os;
    public Browser browser;

    const string toFullString() {
        return browser.toString() ~ "/" ~ os.toString();
    }

    const bool isSpider() {
        return device.family == "Spider";
    }

    const bool isMobile() {
        if(browser.family in mobile_store) {
            return true;
        }
        return false;
    }
}

private Node regexes;

private DeviceStore[] device_store;

private OsStore[] os_store;
private BrowserStore[] browser_store;

private bool[string] mobile_store;

static this() {
    regexes = Loader("../regexes.yaml").load();

    auto user_agent_parsers = regexes["user_agent_parsers"];
    foreach(ref Node user_agent; user_agent_parsers) {
        auto browser = new BrowserStore;
        foreach(ref Node key, ref Node value; user_agent) {
            switch(key.as!string) {
                case "regex":
                    browser.setRegExp(value.as!string);
                break;
                case "family_replacement":
                    browser.setRep(value.as!string);
                break;
                case "v1_replacement":
                    browser.setMajorVersionRep(value.as!string);
                break;
                case "v2_replacement":
                    browser.setMinorVersionRep(value.as!string);
                break;
                default:
                    throw new Exception("Unknown user_agent_parsers key : " ~ key.as!string);
                break;
            }
        }
        browser_store ~= browser;
    }

    auto os_parsers = regexes["os_parsers"];
    foreach(ref Node o; os_parsers) {
        auto os = new OsStore;
        foreach(ref Node key, ref Node value; o) {
            switch(key.as!string) {
                case "regex":
                    os.setRegExp(value.as!string);
                break;
                case "os_replacement":
                    os.setRep(value.as!string);
                break;
                case "os_v1_replacement":
                    os.setMajorVersionRep(value.as!string);
                break;
                case "os_v2_replacement":
                    os.setMinorVersionRep(value.as!string);
                break;
                default:
                    throw new Exception("Unknown os_parsers key : " ~ key.as!string);
                break;
            }
        }
        os_store ~= os;
    }

    auto device_parsers = regexes["device_parsers"];
    foreach(ref Node d; device_parsers) {
        auto device = new DeviceStore;
        foreach(ref Node key, ref Node value; d) {
            switch(key.as!string) {
                case "regex":
                    device.setRegExp(value.as!string);
                break;
                case "device_replacement":
                    device.setRep(value.as!string);
                break;
                default:
                    throw new Exception("Unknown device_parsers key : " ~ key.as!string);
                break;
            }
        }
        device_store ~= device;
    }

    auto mobile_user_agent_families = regexes["mobile_user_agent_families"];
    foreach(ref Node m; mobile_user_agent_families) {
        mobile_store[m.as!string] = true;
    }

    auto mobile_os_families = regexes["mobile_os_families"];
    foreach(ref Node m; mobile_user_agent_families) {
        mobile_store[m.as!string] = true;
    }
}

UserAgent parse(string ua) {
    auto uagent  = new UserAgent;

    auto device  = new Device;

    auto os      = new Os;
    auto browser = new Browser;

    uagent.device  = device;

    uagent.os      = os;
    uagent.browser = browser;

    foreach(b; browser_store) {
        auto r = std.regex.regex(r""~b.getRegExp~"");
        auto m = std.regex.match(ua, r);
        if(!m.empty) {
            try {
                browser.family = b.getRep ? std.regex.replace(
                                                b.getRep,
                                                std.regex.regex(r"\$1"),
                                                m.captures[1]
                                            ) 
                                          : m.captures[1];
            } catch(Throwable e) {
                browser.family = "";
            }
            try {
                browser.major = parseInt(b.getMajorVersionRep ? b.getMajorVersionRep 
                                                              : m.captures[2]);
            } catch(Throwable e) {
                browser.major = 0;
            }
            try {
                browser.minor = parseInt(b.getMinorVersionRep ? b.getMinorVersionRep
                                                              : m.captures[3]);
            } catch(Throwable e) {
                browser.minor = 0;
            }
            try {
                browser.patch = parseInt(m.captures[4]);
            } catch(Throwable e) {
                browser.patch = 0;
            }
            break;
        }
    }

    foreach(o; os_store) {
        auto r = std.regex.regex(r""~o.getRegExp~"");
        auto m = std.regex.match(ua, r);
        if(!m.empty) {
            try {
                os.family = o.getRep  ? std.regex.replace(
                                            o.getRep,
                                            std.regex.regex(r"\$1"),
                                            m.captures[1]
                                        )
                                      : m.captures[1];
            } catch(Throwable e) {
                os.family = "";
            }
            try {
                os.major = parseInt(o.getMajorVersionRep ? o.getMajorVersionRep
                                                         : m.captures[2]);
            } catch(Throwable e) {
                os.major = 0;
            }
            try {
                os.minor = parseInt(o.getMinorVersionRep ? o.getMinorVersionRep
                                                         : m.captures[3]);
            } catch(Throwable e) {
                os.minor = 0;
            }
            try {
                os.patch = parseInt(m.captures[4]);
            } catch(Throwable e) {
                os.patch = 0;
            }
            break;
        }
    }

    foreach(d; device_store) {
        auto r = std.regex.regex(r""~d.getRegExp~"");
        auto m = std.regex.match(ua, r);
        if(!m.empty) {
            try {
                device.family = d.getRep  ? std.regex.replace(
                                                d.getRep,
                                                std.regex.regex(r"\$1"),
                                                m.captures[1]
                                            )
                                          : m.captures[1];
            } catch(Throwable e) {
                device.family = "";
            }
            break;
        }
    }

    return uagent;
}

unittest {
    auto pagent = parse("Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3");

    assert(pagent.browser.family == "Mobile Safari");
    assert(pagent.browser.major == 5);
    assert(pagent.browser.minor == 1);
    assert(pagent.browser.patch == 0);
    assert(pagent.browser.toString == "Mobile Safari 5.1.0");
    assert(pagent.browser.toVersionString == "5.1.0");

    assert(pagent.os.family == "iOS");
    assert(pagent.os.major == 5);
    assert(pagent.os.minor == 1);
    assert(pagent.os.patch == 1);
    assert(pagent.os.toString == "iOS 5.1.1");
    assert(pagent.os.toVersionString == "5.1.1");

    assert(pagent.toFullString == "Mobile Safari 5.1.0/iOS 5.1.1");

    assert(pagent.device.family == "iPhone");
    
    assert(pagent.isSpider == false);
    assert(pagent.isMobile == true);
}