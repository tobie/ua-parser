use HTTP::UA::Parser;
use strict;
use Test::More;

my $OS = 'HTTP::UA::Parser::OS';


is($OS->new('Bar', '4', '3', '2', '1')->toVersionString(), '4.3.2.1');
is($OS->new('Bar', '4', '3', '2')->toVersionString(), '4.3.2');
is($OS->new('Bar', '4', '3')->toVersionString(), '4.3');
is($OS->new('Bar', '4')->toVersionString(), '4');
is($OS->new('Bar')->toVersionString(), '');


is($OS->new('Bar', '4', '3', '2', 'beta')->toVersionString(), '4.3.2beta');
is($OS->new('Bar', '4', '3', 'beta')->toVersionString(), '4.3beta');

is($OS->new('Bar', '4', '3', '2', '1')->toString(), 'Bar 4.3.2.1');

my $parse = $OS->makeParser([{
    regex => '(foo) (\\d)\\.(\\d)\\.(\\d)\\.(\\d)',
    os_replacement => '$1bar',
    os_v1_replacement => 'a',
    os_v2_replacement => 'b',
    os_v3_replacement => 'c',
    os_v4_replacement => 'd'
}]);

ok(ref $parse eq 'CODE');

my $os = $parse->('foo 1.2.3.4');
is ($os->family,'foobar');
is ($os->major,'a');
is ($os->minor,'b');
is ($os->patch,'c');
is ($os->patchMinor,'d');

my $parse2 = $OS->makeParser([{regex => '(foo) (\\d)\\.(\\d).(\\d)\\.(\\d)'}]);
my $os2 = $parse2->('foo 1.2.3.4');
is ($os2->family,'foo');
is ($os2->major,'1');
is ($os2->minor,'2');
is ($os2->patch,'3');
is ($os2->patchMinor,'4');


my $parse3 = $OS->makeParser([{regex => '(foo)'}]);
is($parse3->('foo')->family, 'foo');
done_testing();

