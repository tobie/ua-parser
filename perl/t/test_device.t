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
        my $device = $r->device;
        is ($device->family, $st->{family});
        is ($device->brand, $st->{brand});
        is ($device->model, $st->{model});
    }
};

if ($@){
    diag $@;
    plan skip_all => 'Couldn\'t fetch tests file ' . $source;
}

done_testing();


1;
