Version 0.1 Draft

# ua-parser Specification

This document describes the specification on how a parser should implement the `regexes.yaml` file for correctly parsing user-agent strings on basis of that file. 

This specification shall help maintainers and contributors to correctly use the provided information within the `regexes,yaml` file for obtaining information from the different user-agent strings. Furthermore this specification shall be the basis for discussions on evolving the projects and the needed parsing algorithms.

This document will not provide any information on how to implement the ua-parser project on your server and how to retreive the user-agent string for further processing. 

# regexes.yaml

Any information which can be obtained from a user-agent string may contain information on:

* User-Agent aka "the browser"
* OS (Operating System) the User-Agent currently uses (or runs on)
* Device information by means of the physical device the User-Agent is using

This information is provided within the `regexes.yaml` file. Each kind of information requires a different parser which extracts the related type. These are:

* user_agent_parser
* os_parsers
* device_parsers

Each parser contains a list of regular-expressions which are named `regex`. For each `regex` replacements specific to the parser can be named to attribute or change information. A replacement may require a match from the regular-expression which is extracted by an expression enclosed in normal brackets "()". Each match can be addressed with $1 to $9 and used in a parser specific replacement.

**TODO**: Provide some insights into the used chars. E.g. escape "." as "\." and "(" as "\(". "/" does not need to be escaped.

## user_agent_parsers

The `user_agent_parsers` shall return information of the `family` type of the User-Agent.
If available the version infomation specifying the `family` may be extracted as well if available.
Here major, minor and patch version information can be addressed or overwritten.

| match in regex | default replacement | placeholder in replacement | note    |
| ---- | ------------------- | ---- | --------------------------------------- |
| 1    | family_replacement  | $1   | specifies the User-Agents family        |
| 2    | v1_replacement      | $2   | major version number/info of the family | 
| 3    | v2_replacement      | $3   | minor version number/info of the family | 
| 4    | v3_replacement      | $4   | patch version number/info of the family |

In case that no replacement is specified, the association is given by order of the match. If in the `regex` no first match (within normal brackets) is given, the `family_replacement` shall be specified!
To overwrite the respective value the replacement value needs to be named for a `regex`-item.

**Parser Implementation:**

The list of regular-expressions `regex` shall be evaluated for a given user-agent string beginning with the first `regex`-item in the list to the last item. The first matching `regex` stops processing the list. Regex-matching shall be case sensitive.

In case that no replacement for a match is specified for a `regex`-item, the first match defines the `family`, the second `major`, the third `minor`and the forth `patch` information. 
If a `*_replacement` string is specified it shall overwrite or replace the match. 

As placeholder for inserting matched characters use within 
* `family_replacement`: $1
* `v1_replacement`: $2
* `v2_replacement`: $3
* `v3_replacement`: $4

If no matching `regex` is found the value for `family` shall be "Other". The version information `major`, `minor` and `patch` shall not be defined.

**Example:**

For the User-Agent: `Mozilla/5.0 (Windows; Windows NT 5.1; rv:2.0b3pre) Gecko/20100727 Minefield/4.0.1pre`
the matching `regex`:

````
  - regex: '(Namoroka|Shiretoko|Minefield)/(\d+)\.(\d+)\.(\d+(?:pre)?)'
    family_replacement: 'Firefox ($1)'
````

shall be resolved to:

````
  family: Firefox (Minefield)
  major : 4
  minor : 0
  patch : 1pre
````

## os_parsers

The `os_parsers` shall return information of the `os` type of the Operating System (OS) the User-Agent runs.
If available the version information specifying the `os` may be extracted as well if available.
Here major, minor and patch version information can be addressed or overwritten.

| match in regex | default replacement | placeholder in replacement | note   |
| ---- | ----------------- | ---- | ---------------------------------------- |
| 1    | os_replacement    | $1   | specifies the OS                         |
| 2    | os_v1_replacement | $2   | major version number/info of OS          | 
| 3    | os_v2_replacement | $3   | minor version number/info of the OS      | 
| 4    | os_v3_replacement | $4   | patch version number/info of the OS      |
| 5    | os_v4_replacement | $5   | patchMinor version number/info of the OS |

In case that no replacement is specified, the association is given by order of the match. If in the `regex` no first match (within normal brackets) is given, the `os_replacement` shall be specified!
To overwrite the respective value the replacement value needs to be named for a `regex`-item.

**Parser Implementation:**

The list of regular-expressions `regex` shall be evaluated for a given user-agent string beginning with the first `regex`-item in the list to the last item. The first matching `regex` stops processing the list. Regex-matching shall be case sensitive.

In case that no replacement for a match is specified for a `regex`-item, the first match defines the `os` family, the second `major`, the third `minor`, the forth `patch` and the fifth `patchMinor` version information. 
If a `*_replacement` string is specified it shall overwrite or replace the match. 

As placeholder for inserting matched characters use within 
* `os_replacement` $1
* `os_v1_replacement` $2
* `os_v2_replacement` $3
* `os_v3_replacement` $4
* `os_v4_replacement` $5

In case that no matching `regex` is found the value for `os` shall be "Other". The version information `major`, `minor`, `patch` and `patchMinor` shall not be defined.

**Example:**

For the User-Agent: `Mozilla/5.0 (Windows; U; Win95; en-US; rv:1.1) Gecko/20020826`
the matching `regex`:

````
  - regex: 'Win(95|98|3.1|NT|ME|2000)'
    os_replacement: 'Windows $1'
````

shall be resolved to:

````
  os: Windows 95
````

## device_parsers 

The `device_parsers` shall return information of the device `family` the User-Agent runs.
Furthermore `brand` and `model` of the device can be specified.
`brand` shall name the manufacturer of the device, where model shall specify the model of the device.

| match in regex | default replacement | placeholder in replacement | note   |
| ---- | ------------------ | ------- | ---------------------------------------- |
| 1    | family_replacement | $1...$9 | specifies the device family              |
| any  | brand_replacement  | $1...$9 | major version number/info of OS          | 
| 1    | model_replacement  | $1...$9 | minor version number/info of the OS      | 

In case that no replacement is specified the association is given by order of the match. 
If in the `regex` no first match (within normal brackets) is given the `family_replacement` together with the `model_replacement` shall be specified!
To overwrite the respective value the replacement value needs to be named for a given `regex`.

**Parser Implementation:**

The list of regular-expressions `regex` shall be evaluated for a given user-agent string beginning with the first `regex`-item in the list to the last item. The first matching `regex` stops processing the list. Regex-matching shall be case sensitive.

In case that no replacement for a match is given, the first match defines the `family` and the `model`. 
If a `*_replacement` string is specified it shall overwrite or replace the match. 

As placeholder for inserting matched characters $1 to $9 can be used to insert the matched characters from the regex into the replacement string.

In case that no matching `regex` is found the value for `family` shall be "Other". `brand` and `model` shall not be defined.

**Example:**

For the User-Agent: `Mozilla/5.0 (Linux; U; Android 4.2.2; de-de; PEDI_PLUS_W Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30`
the matching `regex`:

````
  - regex: '; (PEDI)_(PLUS)_(W) Build/'
    device_replacement: '$1_$2_$3'
    brand_replacement: 'Odys'
    model_replacement: '$1 $2 $3'
````

shall be resolved to:

````
  family: 'PEDI_PLUS_W' 
  brand: 'Odys'
  model: 'PEDI PLUS W'
````

# For discussion

For the `device_parsers` some `regex` require case insensitive parsing for proper matching. (E.g. Generic Feature Phones). To distinguish this from the case sensitive default case it is proposed to introduce the value `regex_flag: 'i'` to indicate that the regular-expression matching shall be case-insensitive. 
