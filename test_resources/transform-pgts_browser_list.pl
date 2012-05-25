# Transform http://www.pgts.com.au/download/data/browser_list.txt
# to a YAML format suitable for our needs
use strict;
use utf8;
use open qw(:std :utf8);
binmode(STDOUT, ":utf8");

open(my $fh, '<', 'pgts_browser_list.txt');

print "# From http://www.pgts.com.au/download/data/browser_list.txt
# via http://www.texsoft.it/index.php?m=sw.php.useragent
test_cases:\n";
foreach my $line (<$fh>) {
    $line =~ /(.*)\t(.*)\t(.*)/;
    my $family = '"' . $1 . '"';
    my ($v1, $v2, $v3) = split /\./, $2;

    # Some UAs have double quotes in them :-/
    my $ua = $3;
    $ua =~ s/"/\\"/g;
    $ua = '"' . $ua . '"';

    # Where version field is something like "Camino 0.8"
    my @special = qw(AOL Camino Chimera Epiphany Firebird K-Meleon MultiZilla Phoenix);
    foreach (@special) {
        $family = "'$_'", $v1 = $1 if ($v1 =~ /$_ (\d+)/);
    }

    # Unversioned Firefox
    $family = '"Firefox"', $v1 = '' if ($v1 =~ /Firefox ?/);
    # Mismarked Firefox version
    $v1 = '1' if ($family =~ /Firefox/ && $line =~ /Firefox\/1\.0.*$/);

    $family = '"MultiZilla"', $v1 = '' if ($v1 =~ /MultiZilla/);
    $family = '"IE"' if ($family eq '"MSIE"');

    print "    - user_agent_string: $ua
      family: $family
      major: " . ($v1 eq '' ? "'0'" : "'$v1'") . "
      minor: " . ($v2 eq '' ? '' : "'$v2'") . "
      patch: " . ($v3 eq '' ? '' : "'$v3'") . "\n";
}
