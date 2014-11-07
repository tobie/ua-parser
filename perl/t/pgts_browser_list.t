use HTTP::UA::Parser;
use strict;
use Test::More;
use FindBin qw($Bin);

my $source = 'pgts_browser_list.yaml';
##this is a big file to test
##make tests available for developer only

if ($ENV{TRAVIS} || $ENV{DEV_TESTS}){
    eval {
        require($Bin . '/utils.pl');
        my $yaml = get_test_yaml($source);
        my $r = HTTP::UA::Parser->new();
        foreach my $st (@{$yaml}){
            next if $st->{js_ua};
            $r->parse($st->{user_agent_string});
            my $ua = $r->ua;
            is ($ua->family, $st->{family});
            is ($ua->major, $st->{major});
            is ($ua->minor, $st->{minor});
            is ($ua->patch, $st->{patch});
        }
    };
    
    if ($@){
        diag $@;
        plan skip_all => 'Couldn\'t fetch tests file ' . $source;
    }
} else {
    plan skip_all => 'Set environment DEV_TESTS To run this test';
}

done_testing();

1;
