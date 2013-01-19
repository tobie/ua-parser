# Before `make install' is performed this script should be runnable with
# `make test'. After `make install' it should work as `perl URI-Simple.t'

#########################

# change 'tests => 1' to 'tests => last_test_to_print';

use Test::More 'no_plan';

BEGIN { use_ok('HTTP::UA::Parser') };

my $P = HTTP::UA::Parser->new();

my $agents = {
    
    'Mozilla/5.0 (Windows; U; Windows NT 5.1; de-CH) AppleWebKit/523.15 (KHTML, like Gecko, Safari/419.3) Arora/0.2' => {
        uaFamily => 'Arora',
        uaVersion => '0.2',
        osFamily => 'Windows XP',
        osVersion => undef,
        devFamily => undef
    },
    
    'Mozilla/5.0 (BlackBerry; U; BlackBerry 9700; en-US) AppleWebKit/534.8+ (KHTML, like Gecko) Version/6.0.0.448 Mobile Safari/534.8+' => {
        uaFamily => 'Blackberry WebKit',
        uaVersion => '6.0.0',
        osFamily => 'BlackBerry OS',
        osVersion => '6.0.0.448',
        devFamily => 'BlackBerry 9700'
    },
    
    'Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20120716 Firefox/15.0a2' => {
        uaFamily => 'Firefox Alpha',
        uaVersion => '15.0a2',
        osFamily => 'Windows 7',
        osVersion => undef,
        devFamily => undef
    }
    
};

my $uaTest = sub {
    my $ua = shift;
    my $tests = shift;
    $P->parse($ua);
    
    if (defined $tests->{uaFamily} && $P->ua->family ne $tests->{uaFamily}) {
        #print $P->ua->family;
        return 0;
    }
    if (defined $tests->{uaVersion} && $P->ua->toVersionString ne $tests->{uaVersion}) {
        #print $P->ua->toVersionString;
        return 0;
    }
    if (defined $tests->{osFamily} && $P->os->family ne $tests->{osFamily}){
        #print $P->os->family;
        return 0;
    }
    if (defined $tests->{osVersion} && $P->os->toVersionString ne $tests->{osVersion}) {
        #print $P->os->toVersionString;
        return 0;
    }
    if (defined $tests->{devFamily} && $P->device->family ne $tests->{devFamily}){
        #print $P->device->family;
        return 0;
    }
    return 1;
};

while (my($key,$value) = each %{$agents}) {
    ok( $uaTest->($key,$value), 'Testing User Agent '.$key );
}


1;


__END__
