use HTTP::UA::Parser;
use strict;
use Test::More;
use FindBin qw($Bin);

my $source = 'test_device.yaml';

eval {
    require($Bin . '/utils.pl');
    my $yaml = get_test_yaml($source);
    my $r = HTTP::UA::Parser->new();
    foreach my $st (@{$yaml}){
        $r->parse($st->{user_agent_string});
        my $os = $r->device;
        is ($os->family, $st->{family});
    }
};

if ($@){
    diag $@;
    plan skip_all => 'Couldn\'t fetch tests file ' . $source;
}

done_testing();


1;
