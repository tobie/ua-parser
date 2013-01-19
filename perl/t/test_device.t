use strict;
use FindBin qw($Bin);
use Test::More 'no_plan';

BEGIN { use_ok('HTTP::UA::Parser') };

my $yaml = YAML::Tiny->read( $Bin.'/test_resources/test_device.yaml' )->[0]->{test_cases};
my $r = HTTP::UA::Parser->new();

foreach my $st (@{$yaml}){
    $r->parse($st->{user_agent_string});
    my $family = $st->{family};
    is  ($r->device->family, $family, 'device test '.$family);
}


__END__

