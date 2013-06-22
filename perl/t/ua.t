use HTTP::UA::Parser;
use strict;
use Test::More;

my $UA = 'HTTP::UA::Parser::UA';

is($UA->new()->toString(), 'Other');

is($UA->new('Bar', '4', '3', '2', '1')->toVersionString(), '4.3.2.1');
is($UA->new('Bar', '4', '3', '2')->toVersionString(), '4.3.2');
is($UA->new('Bar', '4', '3')->toVersionString(), '4.3');
is($UA->new('Bar', '4')->toVersionString(), '4');
is($UA->new('Bar')->toVersionString(), '');


is($UA->new('Bar', '4', '3', '2', 'beta')->toVersionString(), '4.3.2beta');
is($UA->new('Bar', '4', '3', 'beta')->toVersionString(), '4.3beta');

is($UA->new('Bar', '4', '3', '2', '1')->toString(), 'Bar 4.3.2.1');

my $parse = $UA->makeParser([{
    regex => '(foo) (\\d)\\.(\\d)\\.(\\d)\\.(\\d)',
    family_replacement => '$1bar',
    v1_replacement => 'a',
    v2_replacement => 'b',
    v3_replacement => 'c',
    v4_replacement => 'd'
}]);

ok(ref $parse eq 'CODE');

my $os = $parse->('foo 1.2.3.4');
is ($os->family,'foobar');
is ($os->major,'a');
is ($os->minor,'b');
is ($os->patch,'c');
is ($os->patchMinor,undef);

my $parse2 = $UA->makeParser([{regex => '(foo) (\\d)\\.(\\d).(\\d)\\.(\\d)'}]);
my $os2 = $parse2->('foo 1.2.3.4');
is ($os2->family,'foo');
is ($os2->major,'1');
is ($os2->minor,'2');
is ($os2->patch,'3');
is ($os2->patchMinor,undef);


my $parse3 = $UA->makeParser([{regex => '(foo)'}]);
is($parse3->('foo')->family, 'foo');
done_testing();

