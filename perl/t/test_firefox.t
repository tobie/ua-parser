use strict;
use warnings;
use FindBin qw($Bin);
#use Yaml::Tiny ();
use Test::More 'no_plan';

BEGIN { use_ok('HTTP::UA::Parser') };

my $yaml = YAML::Tiny->read( $Bin.'/test_resources/firefox_user_agent_strings.yaml' )->[0]->{test_cases};


#print Dumper $yaml;
my $r = HTTP::UA::Parser->new();


foreach my $st (@{$yaml}){
    
    #print $st->{user_agent_string} . "\n";
    
    $r->parse($st->{user_agent_string});
    
    my $browser = $r->ua;
    
    #print $browser->patch() . "\n";
    #print $st->{patch};
    is ($browser->family, $st->{family});
    is ($browser->major,$st->{major});
    is ($browser->minor,$st->{minor});
    is ($browser->patch,$st->{patch}, 'firefox '. $st->{family});
}

__END__
