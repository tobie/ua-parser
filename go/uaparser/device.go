package uaparser

import (
	"regexp"
	"strings"
)

type Device struct {
	Family string
}

type DevicePattern struct {
	Regexp            *regexp.Regexp
	Regex             string
	DeviceReplacement string
}

func (dvcPattern *DevicePattern) Match(line string, dvc *Device) {
	bytes := dvcPattern.Regexp.FindStringSubmatch(line)
	if len(bytes) > 0 {
		groupCount := dvcPattern.Regexp.NumSubexp()

		if len(dvcPattern.DeviceReplacement) > 0 {
			if strings.Contains(dvcPattern.DeviceReplacement, "$1") && groupCount >= 1 && len(bytes) >= 2 {
				dvc.Family = strings.Replace(dvcPattern.DeviceReplacement, "$1", bytes[1], 1)
			} else {
				dvc.Family = dvcPattern.DeviceReplacement
			}
		} else if groupCount >= 1 {
			dvc.Family = bytes[1]
		}
	}
}

func (dvc *Device) ToString() string {
	return dvc.Family
}
