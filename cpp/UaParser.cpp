#include "UaParser.h"

#include <fstream>
#include <string>
#include <vector>
#include <unordered_set>

#include <boost/regex.hpp>
#include <glog/logging.h>
#include <yaml-cpp/yaml.h>

namespace {

struct DeviceStore {
  std::string replacement;
  boost::regex regExpr;
};

struct AgentStore : DeviceStore {
  std::string majorVersionReplacement;
  std::string minorVersionReplacement;
};

typedef AgentStore OsStore;
typedef AgentStore BrowserStore;

#define FILL_AGENT_STORE(node, agent_store, repl, maj_repl, min_repl)    \
    CHECK(node.Type() == YAML::NodeType::Map);                           \
    for (auto it = node.begin(); it != node.end(); ++it) {               \
      const std::string key = it.first().to<std::string>();              \
      const std::string value = it.second().to<std::string>();           \
      if (key == "regex") {                                              \
        agent_store.regExpr = value;                                     \
      } else if (key == repl) {                                          \
        agent_store.replacement = value;                                \
      } else if (key == maj_repl && !value.empty()) {                    \
        agent_store.majorVersionReplacement = value;                     \
      } else if (key == min_repl && !value.empty()) {                    \
        try {                                                            \
          agent_store.minorVersionReplacement = value;                   \
        } catch (...) {}                                                 \
      } else {                                                           \
        CHECK(false);                                                    \
      }                                                                  \
    }

struct UAStore {
  explicit UAStore(const std::string& regexes_file_path) {
    std::ifstream in_stream(regexes_file_path);
    CHECK(in_stream.good());
    
    YAML::Parser yaml_parser(in_stream);
    YAML::Node regexes;
    CHECK(yaml_parser.GetNextDocument(regexes));

    const auto& user_agent_parsers = regexes["user_agent_parsers"];
    for (const auto& user_agent : user_agent_parsers) {
      BrowserStore browser;
      FILL_AGENT_STORE(user_agent, browser, "family_replacement",
        "v1_replacement", "v2_replacement");
      browserStore.push_back(browser);
    }

    const auto& os_parsers = regexes["os_parsers"];
    for (const auto& o : os_parsers) {
      OsStore os;
      FILL_AGENT_STORE(o, os, "os_replacement", "os_v1_replacement",
        "os_v2_replacement");
      osStore.push_back(os);
    }

    const auto& device_parsers = regexes["device_parsers"];
    for (const auto& d : device_parsers) {
      DeviceStore device;
      for (auto it = d.begin(); it != d.end(); ++it) {
        const std::string key = it.first().to<std::string>();
        const std::string value = it.second().to<std::string>();
        if (key == "regex") {
          device.regExpr = value;
        } else if (key == "device_replacement") {
          device.replacement = value;
        } else {
          CHECK(false);
        }
      }
      deviceStore.push_back(device);
    }
  }

  std::vector<DeviceStore> deviceStore;
  std::vector<OsStore> osStore;
  std::vector<BrowserStore> browserStore;
};

template<class AGENT, class AGENT_STORE>
void fillAgent(AGENT& agent, const AGENT_STORE& store, const boost::smatch& m) {
  CHECK(!m.empty());
  if (m.size() > 1) {
    agent.family = !store.replacement.empty()
      ? boost::regex_replace(store.replacement, boost::regex("\\$1"), m[1].str())
      : m[1];
  } else {
    agent.family = !store.replacement.empty()
      ? boost::regex_replace(store.replacement, boost::regex("\\$1"), m[0].str())
      : m[0];
  }
  if (!store.majorVersionReplacement.empty()) {
    agent.major = store.majorVersionReplacement;
  } else if (m.size() > 2) {
    const auto s = m[2].str();
    if (!s.empty()) {
      agent.major = s;
    }
  }
  if (!store.minorVersionReplacement.empty()) {
    agent.minor = store.minorVersionReplacement;
  } else if (m.size() > 3) {
    const auto s = m[3].str();
    if (!s.empty()) {
      agent.minor = s;
    }
  }
  if (m.size() > 4) {
    const auto s = m[4].str();
    if (!s.empty()) {
      agent.patch = s;
    }
  }
}

UserAgent parseImpl(const std::string& ua, const UAStore* ua_store) {
  UserAgent uagent;

  for (const auto& b : ua_store->browserStore) {
    auto& browser = uagent.browser;
    boost::smatch m;
    if (boost::regex_search(ua, m, b.regExpr)) {
      fillAgent(browser, b, m);
      break;
    } else {
      browser.family = "Other";
    }
  }

  for (const auto& o : ua_store->osStore) {
    auto& os = uagent.os;
    boost::smatch m;
    if (boost::regex_search(ua, m, o.regExpr)) {
      fillAgent(os, o, m);
      break;
    } else {
      os.family = "Other";
    }
  }

  for (const auto& d : ua_store->deviceStore) {
    auto& device = uagent.device;
    boost::smatch m;
    if (boost::regex_search(ua, m, d.regExpr)) {
      if (m.size() > 1) {
        device.family = !d.replacement.empty()
          ? boost::regex_replace(d.replacement, boost::regex("\\$1"), m[1].str())
          : m[1].str();
      } else if (m.size() == 1) {
        device.family = !d.replacement.empty()
          ? boost::regex_replace(d.replacement, boost::regex("\\$1"), m[0].str())
          : m[0].str();
      }
      break;
    } else {
      device.family = "Other";
    }
  }

  return uagent;
}

}  // namespace

UserAgentParser::UserAgentParser(const std::string& regexes_file_path)
  : regexes_file_path_ { regexes_file_path } {
  ua_store_ = new UAStore(regexes_file_path);
}

UserAgentParser::~UserAgentParser() {
  delete static_cast<const UAStore*>(ua_store_);
}

UserAgent UserAgentParser::parse(const std::string& ua) const {
  return parseImpl(ua, static_cast<const UAStore*>(ua_store_));
}
