use HTTP::UA::Parser;
use strict;
use Test::More;
use FindBin qw($Bin);

my $source = 'test_user_agent_parser_os.yaml';

eval {
    require($Bin . '/utils.pl');
    my $yaml = get_test_yaml($source);
    my $r = HTTP::UA::Parser->new();
    foreach my $st (@{$yaml}){
        next if $st->{js_ua};
        $r->parse($st->{user_agent_string});
        my $os = $r->os;
        is ($os->family, $st->{family});
        is ($os->major, $st->{major});
        is ($os->minor, $st->{minor},$os->family);
        is ($os->patch, $st->{patch});
        is ($os->patchMinor, $st->{patch_minor});
    }
};

if ($@){
    diag $@;
    plan skip_all => 'Couldn\'t fetch tests file ' . $source;
}

done_testing();


1;
