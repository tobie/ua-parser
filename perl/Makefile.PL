use 5.010001;
use ExtUtils::MakeMaker;
use FindBin qw($Bin);
use File::Copy;

# See lib/ExtUtils/MakeMaker.pm for details of how to influence
# the contents of the Makefile that is written.
My_WriteMakefile(
    NAME              => 'HTTP::UA::Parser',
    VERSION_FROM      => 'lib/HTTP/UA/Parser.pm', # finds $VERSION
    EXE_FILES    => ['script/ua_parser'],
    PREREQ_PM         => {
	'YAML::Tiny' => 0
    },
    ($] >= 5.005 ?     ## Add these new keywords supported since 5.005
      (ABSTRACT_FROM  => 'lib/HTTP/UA/Parser.pm', # retrieve abstract from module
       AUTHOR         => 'Mamod A. Mehyar <mamod.mehyar@gmail.com>') : ())
);

sub My_WriteMakefile {
    my %params=@_;
    my $regexes = '../regexes.yaml';
    my $regexes2 = $Bin.'/regexes.yaml';
    my $dest = $Bin.'/lib/HTTP/UA/Parser';
    if (-e $regexes) {
	copy($regexes, $dest) or die "File cannot be copied.";
    } elsif (-e $regexes2) {
	copy($regexes2, $dest) or die "File cannot be copied.";
    } else {
	print "Can't locate regexes.yaml file\n";
	print "We will try to fetch it from a remote server\n";
	my $regexContent = getRegexes();
	open my $file,'>',$regexes2 or die "can't open file $!";
	print $file $regexContent;
	close $file;
	copy($regexes2, $dest) or die "File cannot be copied.";
	print "regexes.yaml downloaded successfully\n";
    }
    
    WriteMakefile(%params);
}

##also update in script/ua_parser if changed here
sub getRegexes {
    my $response;
    my $stream;
    my $url = 'https://raw.github.com/tobie/ua-parser/master/regexes.yaml';
    #trying curl
    print "Trying curl\n";
    open $stream, "-|", "curl $url" or die;
    while(<$stream>) { $response .= "$_" };
    
    ##trying wget
    if (!$response){
	print "Trying wget\n";
	open $stream, "-|", "wget $url" or die;
	while(<$stream>) { $response .= "$_" };
    }
    
    ##trying lwp-request
    if (!$response){
	print "Trying lwp-request\n";
	open $stream, "-|", "lwp-request $url" or die;
	while(<$stream>) { $response .= "$_" };
    }
    
    return $response if $response;
    print "Trying to fetch using LWP::UserAgent\n";
    eval "use LWP::UserAgent";
    if ($@){
	print "We couldn't locate LWP::UserAgent Module\n";
	print "LWP::UserAgent required to fetch regexes.yaml from server\n";
	print "Please install it or get regexes.yaml file manually from\n";
	print "https://raw.github.com/tobie/ua-parser/master/regexes.yaml\n";
	print "and place it in the root folder of this distro\n";
	print "then run Makefile.PL again\n";
	exit;
    }
    my $ua = LWP::UserAgent->new;
    $ua->timeout(5);
    $ua->env_proxy();
    $response = $ua->get($url);
    if ($response->is_success) {
        return $response->content;
    } else {
        print "Request aborted\n";
        exit;
    }
}