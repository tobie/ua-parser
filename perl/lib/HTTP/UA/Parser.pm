package HTTP::UA::Parser;
use strict;
use warnings;
use YAML::Tiny 'LoadFile';
our $VERSION = '0.004';
my ($REGEX,$PATH);
my $PACKAGE = __PACKAGE__;

sub new {
    my ($class,$op) = @_;
    my ($ua,$path);
    if (ref $op eq 'HASH'){
	$path = $op->{regex};
	$ua = $op->{ua};
    } else { $ua = $op; }
    
    if (!$REGEX){
	if ($path){
	    $REGEX ||= LoadFile( $path );
	} else {
	    $PATH = HTTP::UA::Parser::Utils::getPath();
	    my $regFile;
	    if (-e ($regFile = $PATH.'../../../../../../regexes.yaml')){}
	    elsif (-e ($regFile = $PATH.'/regexes.yaml')){}
	    else {
		die
		"Can't find regexes.yaml file\n".
		"you can download/update it using command line by typing\n".
		"    % ua_parser -u\n".
		"or simply download it from\n".
		"https://raw.github.com/tobie/ua-parser/master/regexes.yaml".
		"and include it as an option when construct new HTTP::UA::Parser class\n".
		"ex ->new({regex => '/full/path/to/regexes.yaml'})";
	    }
	    $REGEX = LoadFile( $regFile );
	}
    }
    
    my $self = {
        user_agent => $ua || $ENV{HTTP_USER_AGENT},
	path => $PATH
    };
    
    return bless($self,$class);
}

sub parse {
    my $self = shift;
    $self->{user_agent} = $_[0];
    $self->{os} = undef;
    $self->{ua} = undef;
    $self->{device} = undef;
    return $self;
}

sub os {
    my ($self) = @_;
    $self->{os} ||= HTTP::UA::Parser::OS->parse($self->{user_agent});
    return $self->{os};
}

sub ua {
    my ($self) = @_;
    $self->{ua} ||= HTTP::UA::Parser::UA->parse($self->{user_agent});
    return $self->{ua};
}

sub device {
    my ($self) = @_;
    $self->{device} ||= HTTP::UA::Parser::Device->parse($self->{user_agent});
    return $self->{device};
}

##=============================================================================
## UA Package
##=============================================================================
package HTTP::UA::Parser::UA;

sub new {HTTP::UA::Parser::Base::new(@_)}

sub parse {
    my $self = shift;
    my $ua = shift;
    my $regexes = $REGEX->{user_agent_parsers};
    my $parser = $self->makeParser($regexes);
    return $parser->($ua);
}

sub makeParser {
    my $self = shift;
    my $regexes = shift;
    return HTTP::UA::Parser::Utils::makeParser($regexes,\&_makeParsers);
}

sub _makeParsers {
    
    my ($obj) = shift;
    my $regexp = $obj->{regex};
    my $famRep = $obj->{family_replacement};
    my $majorRep = $obj->{v1_replacement};
    my $minorRep = $obj->{v2_replacement};
    my $patchRep = $obj->{v3_replacement};
    
    my $parser = sub {
        my $str = shift;
        my @m = HTTP::UA::Parser::Utils::exe( $regexp , $str );
        if (!@m) { return undef; }
        my $family = defined $famRep ? HTTP::UA::Parser::Utils::replace($famRep,qr/\$1/,$m[0]) : $m[0];
        my $major = defined $majorRep ?  $majorRep : $m[1];
        my $minor = defined $minorRep ?  $minorRep : $m[2];
        my $patch = defined $patchRep ?  $patchRep : $m[3];
        return ($family, $major, $minor, $patch);
    };
    
    return $parser;
}

##=============================================================================
## OS Package
##=============================================================================
package HTTP::UA::Parser::OS;

sub new {HTTP::UA::Parser::Base::new(@_)}

sub parse {
    my $self = shift;
    my $ua = shift;
    my $regexes = $REGEX->{os_parsers};
    my $parser = $self->makeParser($regexes);
    return $parser->($ua);
}

sub makeParser {
    my $self = shift;
    my $regexes = shift;
    return HTTP::UA::Parser::Utils::makeParser($regexes,\&_makeParsers);
}

sub _makeParsers {
    
    my ($obj) = shift;
    my $regexp = $obj->{regex};
    my $famRep = $obj->{os_replacement};
    my $majorRep = $obj->{os_v1_replacement};
    my $minorRep = $obj->{os_v2_replacement};
    my $patchRep = $obj->{os_v3_replacement};
    my $patchMinorRep = $obj->{os_v4_replacement};
    
    my $parser = sub {
        my $str = shift;
        my @m = HTTP::UA::Parser::Utils::exe( $regexp , $str );
        if (!@m) { return undef; }
        my $family = $famRep ? HTTP::UA::Parser::Utils::replace($famRep,qr/\$1/,$m[0]) : $m[0];
        my $major = defined $majorRep ? $majorRep : $m[1];
        my $minor = defined $minorRep ? $minorRep : $m[2];
        my $patch = defined $patchRep ? $patchRep : $m[3];
        my $patchMinor = defined $patchMinorRep ? $patchMinorRep : $m[4];
        return ($family, $major, $minor, $patch, $patchMinor);
    };
    
    return $parser;
}

##=============================================================================
## Device Package
##=============================================================================
package HTTP::UA::Parser::Device;

sub new {HTTP::UA::Parser::Base::new(@_)}

sub makeParser {
    my $self = shift;
    my $regexes = shift;
    return HTTP::UA::Parser::Utils::makeParser($regexes,\&_makeParsers);
}

sub parse {
    my $self = shift;
    my $ua = shift;
    my $regexes = $REGEX->{device_parsers};
    my $parser = $self->makeParser($regexes);
    return $parser->($ua);
}

sub _makeParsers {
    my ($obj) = shift;
    my $regexp = $obj->{regex};
    my $deviceRep = $obj->{device_replacement};
    my $parser = sub {
        my $str = shift;
        my @m = HTTP::UA::Parser::Utils::exe( $regexp , $str );
        if (!@m) { return undef; }
        my $family = $deviceRep ? HTTP::UA::Parser::Utils::replace($deviceRep,qr/\$1/,$m[0]) : $m[0];
        return ($family);
    };
    return $parser;
}
##=============================================================================
## Stringify Package
##=============================================================================
package HTTP::UA::Parser::Base;

sub new {
    my $class = shift;
    my $self = {
	family => $_[0] || 'Other',
	major => $_[1],
	minor => $_[2],
	patch => $_[3],
	patchMinor => $_[4]
    };
    return bless($self, __PACKAGE__ );
}

sub toVersionString {
    my $self  = shift;
    my $output = '';
    if (defined $self->{major}){
	$output .= $self->{major};
	if (defined $self->{minor}){
	    $output .= '.' . $self->{minor};
	    if (defined $self->{patch}) {
		if (HTTP::UA::Parser::Utils::startsWithDigit($self->{patch})) { $output .= '.'; }
		$output .= $self->{patch};
		if (defined $self->{patchMinor}) {
		    if (HTTP::UA::Parser::Utils::startsWithDigit($self->{patchMinor})) { $output .= '.'; }
		    $output .= $self->{patchMinor};
		}
	    }
	}
    }
    return $output;
}

sub toString {
    my $self = shift;
    my $suffix = $self->toVersionString();
    if ($suffix){
	$suffix = ' ' . $suffix;
    }
    return $self->family . $suffix;
}

sub family	{	shift->{family}		}
sub major	{	shift->{major}		}
sub minor  	{	shift->{minor}		}
sub patch  	{	shift->{patch}		}
sub patchMinor	{	shift->{patchMinor}	}

##=============================================================================
## Utils Package
##=============================================================================
package HTTP::UA::Parser::Utils;

sub makeParser {
    my $regexes = shift;
    my $makeParser = shift || \&_makeparser;
    my @parsers = map {
        $makeParser->($_);
    } @{$regexes};
    
    my $parser = sub {
	my $ua = shift;
	my @obj;
	foreach my $parser (@parsers){
	    @obj = $parser->($ua);
	    return HTTP::UA::Parser::Base->new(@obj) if $obj[0];
	}
	
	HTTP::UA::Parser::Base->new();
    };
    
    return $parser;
}

sub _makeParsers {
    
    my ($obj) = shift;
    my $regexp = $obj->{regex};
    my $famRep = $obj->{family_replacement};
    my $majorRep = $obj->{v1_replacement};
    my $minorRep = $obj->{v2_replacement};
    my $patchRep = $obj->{v3_replacement};
    
    my $parser = sub {
        my $str = shift;
        my @m = HTTP::UA::Parser::Utils::exe( $regexp , $str );
        if (!@m) { return undef; }
        my $family = defined $famRep ? replace($famRep,qr/\$1/,$m[0]) : $m[0];
        my $major = defined $majorRep ?  $majorRep : $m[1];
        my $minor = defined $minorRep ?  $minorRep : $m[2];
        my $patch = defined $patchRep ?  $patchRep : $m[3];
        return ($family, $major, $minor, $patch);
    };
    
    return $parser;
}

sub replace {
    my ($stringToReplace,$expr,$replaceWith) = @_;
    $stringToReplace =~ s/$expr/$replaceWith/;
    return $stringToReplace;
}

sub exe {
    my ($expr,$string) = @_;
    my @m = $string =~ $expr;
    return @m;
}

sub startsWithDigit {
    my $str = shift;
    return $str =~ /^\d/;
}

sub getPath {
    $PATH = $PACKAGE;
    $PATH =~ s/::/\//g;
    $PATH .= '.pm';
    $PATH = $INC{$PATH};
    $PATH =~ s/.pm$//;
    return $PATH;
}

1;

__END__

=pod

=head1 NAME

HTTP::UA::Parser - Perl User Agent Parser

=head1 DESCRIPTION

Perl port of the ua-parser project - L<https://github.com/tobie/ua-parser>.

=head1 SYNOPSIS

    use HTTP::UA::Parser;
    my $r = HTTP::UA::Parser->new();
    
    print $r->ua->toString();         # -> "Safari 5.0.1"
    print $r->ua->toVersionString();  # -> "5.0.1"
    print $r->ua->family;             # -> "Safari"
    print $r->ua->major;              # -> "5"
    print $r->ua->minor;              # -> "0"
    print $r->ua->patch;              # -> "1"
    
    print $r->os->toString();         # -> "iOS 5.1"
    print $r->os->toVersionString();  # -> "5.1"
    print $r->os->family              # -> "iOS"
    print $r->os->major;              # -> "5"
    print $r->os->minor;              # -> "1"
    print $r->os->patch;              # -> undef
    
    print $r->device->family;         # -> "iPhone"
    
=head1 Methods

=head2 new()

Accepts a user agent string to parse, leave empty to parse caller user agent.

=head2 parse()

Accepts a new user agent to parse

=head2 ua()

Parses browser part of the user agent

=head2 os()

Parsers operating system part of the user agent

=head2 device()

Parses device part of the user agent

=head1 Strigify Methods

Methods to print results as strings

=over 4

=item toString()

returns os / ua name

=item toVersionString()

returns full version number of os/browser

=item family()

returns family name of os/browser/device

=item major()

returns version's major part of os/browser

=item minor()

returns version's minor part of os/browser

=item patch()

returns versions patch part of os/browser

=item patchMinor()

returns version patch minor part of os/browser

=back
    
=head1 INSTALLATION

From CPAN shell simply type

    % perl -MCPAN -e 'install HTTP::UA::Parser'

Or from your local download, unpack and:

    % perl Makefile.PL
    % make && make test
    
Then install:

    % make install

=head1 AUTHOR

Mamod A. Mehyar, E<lt>mamod.mehyar@gmail.comE<gt>

=head1 COPYRIGHT AND LICENSE

Copyright (C) 2013 by Mamod A. Mehyar

This library is free software; you can redistribute it and/or modify
it under the same terms as Perl itself, either Perl version 5.10.1 or,
at your option, any later version of Perl 5 you may have available.

=cut
