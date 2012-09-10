import UaParser;

import std.stdio;

void main() {
    auto agent = UaParser.parse("Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3");
    std.stdio.writeln(agent.browser.family);
    std.stdio.writeln(agent.browser.major);
    std.stdio.writeln(agent.browser.minor);
    std.stdio.writeln(agent.browser.patch);
    std.stdio.writeln(agent.browser.toString);
    std.stdio.writeln(agent.browser.toVersionString);

    std.stdio.writeln("=================================");

    std.stdio.writeln(agent.os.family);
    std.stdio.writeln(agent.os.major);
    std.stdio.writeln(agent.os.minor);
    std.stdio.writeln(agent.os.patch);
    std.stdio.writeln(agent.os.toString);
    std.stdio.writeln(agent.os.toVersionString);

    std.stdio.writeln("=================================");

    std.stdio.writeln(agent.toFullString);

    std.stdio.writeln("=================================");
    
    std.stdio.writeln(agent.device.family);
    
    std.stdio.writeln(agent.isMobile);
    std.stdio.writeln(agent.isSpider);
}