use HTTP::UA::Parser;
use strict;
use Test::More;

my $Device = 'HTTP::UA::Parser::Device';

my $device = $Device->new();
is($device->family, 'Other');
is($device->toString(), 'Other');

my $parse = $Device->makeParser([{regex => '(foo)'}]);
is($parse->('foo')->family,'foo');
is($parse->('bar')->family,'Other');

my $parse2 = $Device->makeParser([{
    regex => '(foo)',
    device_replacement => '$1bar'
}]);

is($parse2->('foo')->family,'foobar');

done_testing();
