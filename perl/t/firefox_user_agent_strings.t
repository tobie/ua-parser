use HTTP::UA::Parser;
use strict;
use Test::More;
use FindBin qw($Bin);

my $source = 'firefox_user_agent_strings.yaml';

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

done_testing();

1;
