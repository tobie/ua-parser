use strict;
use FindBin qw($Bin);
use Test::More 'no_plan';

BEGIN { use_ok('HTTP::UA::Parser') };

my $yaml = YAML::Tiny->read( $Bin.'/test_resources/test_user_agent_parser.yaml' )->[0]->{test_cases};
my $r = HTTP::UA::Parser->new();

foreach my $st (@{$yaml}){
    $r->parse($st->{user_agent_string});
    my $ua = $r->ua;
    #print $ua->family . "\n";
    is ($ua->family, $st->{family});
    is ($ua->major, $st->{major});
    is ($ua->minor, $st->{minor});
    is ($ua->patch, $st->{patch}, 'OS '. $st->{family});
}


__END__
