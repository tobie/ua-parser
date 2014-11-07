#!perl
use YAML::Tiny;
use LWP::UserAgent;

my $resources = 'https://raw.github.com/tobie/ua-parser/master/test_resources/';

sub get_test_yaml {
    my $file = shift;
    my $content;
    
    my $ua = LWP::UserAgent->new;
    my $req = HTTP::Request->new(GET => $resources . $file);
    my $res = $ua->request($req);
    die if !$res->is_success;
    $content = $res->content;
    die if !$content;
    
    my $yaml = fix_yaml($content);
    return YAML::Tiny->read_string( $yaml )->[0]->{test_cases};
}

sub fix_yaml {
    my $yaml = shift;
    open(my $fh,'<', \$yaml );
    my $c;
    while(my $line = <$fh>){
        if ($line){
            $line =~ s/(\s*(?:family|major|minor|patch|patch_minor):)\s?\n$/$1 ~\n/;
        }
        $c .= $line;
    }
    close $fh;
    return $c;
}


1;
