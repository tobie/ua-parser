#pragma once

#include <string>

struct Device {
  std::string family;
};

struct Agent : Device {
  std::string major;
  std::string minor;
  std::string patch;

  std::string toString() const {
    return family + " " + toVersionString();
  }

  std::string toVersionString() const {
    return (major.empty() ? "0" : major) + "." +
      (minor.empty() ? "0" : minor) + "." +
      (patch.empty() ? "0" : patch);
  }
};

typedef Agent Os;
typedef Agent Browser;

struct UserAgent {
  Device device;

  Os os;
  Browser browser;

  std::string toFullString() const {
    return browser.toString() + "/" + os.toString();
  }

  bool isSpider() const {
    return device.family == "Spider";
  }
};

class UserAgentParser {
 public:
  explicit UserAgentParser(const std::string& regexes_file_path);

  UserAgent parse(const std::string&) const;

  ~UserAgentParser();

 private:
  const std::string regexes_file_path_;
  const void* ua_store_;
};
