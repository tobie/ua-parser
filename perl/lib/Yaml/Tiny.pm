package YAML::Tiny;

use strict;

# UTF Support?
sub HAVE_UTF8 () { $] >= 5.007003 }
BEGIN {
	if ( HAVE_UTF8 ) {
		# The string eval helps hide this from Test::MinimumVersion
		eval "require utf8;";
		die "Failed to load UTF-8 support" if $@;
	}

	# Class structure
	require 5.004;
	require Exporter;
	require Carp;
	$YAML::Tiny::VERSION   = '1.51';
	# $YAML::Tiny::VERSION   = eval $YAML::Tiny::VERSION;
	@YAML::Tiny::ISA       = qw{ Exporter  };
	@YAML::Tiny::EXPORT    = qw{ Load Dump };
	@YAML::Tiny::EXPORT_OK = qw{ LoadFile DumpFile freeze thaw };

	# Error storage
	$YAML::Tiny::errstr    = '';
}

# The character class of all characters we need to escape
# NOTE: Inlined, since it's only used once
# my $RE_ESCAPE = '[\\x00-\\x08\\x0b-\\x0d\\x0e-\\x1f\"\n]';

# Printed form of the unprintable characters in the lowest range
# of ASCII characters, listed by ASCII ordinal position.
my @UNPRINTABLE = qw(
	z    x01  x02  x03  x04  x05  x06  a
	x08  t    n    v    f    r    x0e  x0f
	x10  x11  x12  x13  x14  x15  x16  x17
	x18  x19  x1a  e    x1c  x1d  x1e  x1f
);

# Printable characters for escapes
my %UNESCAPES = (
	z => "\x00", a => "\x07", t    => "\x09",
	n => "\x0a", v => "\x0b", f    => "\x0c",
	r => "\x0d", e => "\x1b", '\\' => '\\',
);

# Special magic boolean words
my %QUOTE = map { $_ => 1 } qw{
	null Null NULL
	y Y yes Yes YES n N no No NO
	true True TRUE false False FALSE
	on On ON off Off OFF
};





#####################################################################
# Implementation

# Create an empty YAML::Tiny object
sub new {
	my $class = shift;
	bless [ @_ ], $class;
}

# Create an object from a file
sub read {
	my $class = ref $_[0] ? ref shift : shift;

	# Check the file
	my $file = shift or return $class->_error( 'You did not specify a file name' );
	return $class->_error( "File '$file' does not exist" )              unless -e $file;
	return $class->_error( "'$file' is a directory, not a file" )       unless -f _;
	return $class->_error( "Insufficient permissions to read '$file'" ) unless -r _;

	# Slurp in the file
	local $/ = undef;
	local *CFG;
	unless ( open(CFG, $file) ) {
		return $class->_error("Failed to open file '$file': $!");
	}
	my $contents = <CFG>;
	unless ( close(CFG) ) {
		return $class->_error("Failed to close file '$file': $!");
	}

	$class->read_string( $contents );
}

# Create an object from a string
sub read_string {
	my $class  = ref $_[0] ? ref shift : shift;
	my $self   = bless [], $class;
	my $string = $_[0];
	eval {
		unless ( defined $string ) {
			die \"Did not provide a string to load";
		}

		# Byte order marks
		# NOTE: Keeping this here to educate maintainers
		# my %BOM = (
		#     "\357\273\277" => 'UTF-8',
		#     "\376\377"     => 'UTF-16BE',
		#     "\377\376"     => 'UTF-16LE',
		#     "\377\376\0\0" => 'UTF-32LE'
		#     "\0\0\376\377" => 'UTF-32BE',
		# );
		if ( $string =~ /^(?:\376\377|\377\376|\377\376\0\0|\0\0\376\377)/ ) {
			die \"Stream has a non UTF-8 BOM";
		} else {
			# Strip UTF-8 bom if found, we'll just ignore it
			$string =~ s/^\357\273\277//;
		}

		# Try to decode as utf8
		utf8::decode($string) if HAVE_UTF8;

		# Check for some special cases
		return $self unless length $string;
		unless ( $string =~ /[\012\015]+\z/ ) {
			die \"Stream does not end with newline character";
		}

		# Split the file into lines
		my @lines = grep { ! /^\s*(?:\#.*)?\z/ }
			    split /(?:\015{1,2}\012|\015|\012)/, $string;

		# Strip the initial YAML header
		@lines and $lines[0] =~ /^\%YAML[: ][\d\.]+.*\z/ and shift @lines;

		# A nibbling parser
		while ( @lines ) {
			# Do we have a document header?
			if ( $lines[0] =~ /^---\s*(?:(.+)\s*)?\z/ ) {
				# Handle scalar documents
				shift @lines;
				if ( defined $1 and $1 !~ /^(?:\#.+|\%YAML[: ][\d\.]+)\z/ ) {
					push @$self, $self->_read_scalar( "$1", [ undef ], \@lines );
					next;
				}
			}

			if ( ! @lines or $lines[0] =~ /^(?:---|\.\.\.)/ ) {
				# A naked document
				push @$self, undef;
				while ( @lines and $lines[0] !~ /^---/ ) {
					shift @lines;
				}

			} elsif ( $lines[0] =~ /^\s*\-/ ) {
				# An array at the root
				my $document = [ ];
				push @$self, $document;
				$self->_read_array( $document, [ 0 ], \@lines );

			} elsif ( $lines[0] =~ /^(\s*)\S/ ) {
				# A hash at the root
				my $document = { };
				push @$self, $document;
				$self->_read_hash( $document, [ length($1) ], \@lines );

			} else {
				die \"YAML::Tiny failed to classify the line '$lines[0]'";
			}
		}
	};
	if ( ref $@ eq 'SCALAR' ) {
		return $self->_error(${$@});
	} elsif ( $@ ) {
		require Carp;
		Carp::croak($@);
	}

	return $self;
}

# Deparse a scalar string to the actual scalar
sub _read_scalar {
	my ($self, $string, $indent, $lines) = @_;

	# Trim trailing whitespace
	$string =~ s/\s*\z//;

	# Explitic null/undef
	return undef if $string eq '~';

	# Single quote
	if ( $string =~ /^\'(.*?)\'(?:\s+\#.*)?\z/ ) {
		return '' unless defined $1;
		$string = $1;
		$string =~ s/\'\'/\'/g;
		return $string;
	}

	# Double quote.
	# The commented out form is simpler, but overloaded the Perl regex
	# engine due to recursion and backtracking problems on strings
	# larger than 32,000ish characters. Keep it for reference purposes.
	# if ( $string =~ /^\"((?:\\.|[^\"])*)\"\z/ ) {
	if ( $string =~ /^\"([^\\"]*(?:\\.[^\\"]*)*)\"(?:\s+\#.*)?\z/ ) {
		# Reusing the variable is a little ugly,
		# but avoids a new variable and a string copy.
		$string = $1;
		$string =~ s/\\"/"/g;
		$string =~ s/\\([never\\fartz]|x([0-9a-fA-F]{2}))/(length($1)>1)?pack("H2",$2):$UNESCAPES{$1}/gex;
		return $string;
	}

	# Special cases
	if ( $string =~ /^[\'\"!&]/ ) {
		die \"YAML::Tiny does not support a feature in line '$string'";
	}
	return {} if $string =~ /^{}(?:\s+\#.*)?\z/;
	return [] if $string =~ /^\[\](?:\s+\#.*)?\z/;

	# Regular unquoted string
	if ( $string !~ /^[>|]/ ) {
		if (
			$string =~ /^(?:-(?:\s|$)|[\@\%\`])/
			or
			$string =~ /:(?:\s|$)/
		) {
			die \"YAML::Tiny found illegal characters in plain scalar: '$string'";
		}
		$string =~ s/\s+#.*\z//;
		return $string;
	}

	# Error
	die \"YAML::Tiny failed to find multi-line scalar content" unless @$lines;

	# Check the indent depth
	$lines->[0]   =~ /^(\s*)/;
	$indent->[-1] = length("$1");
	if ( defined $indent->[-2] and $indent->[-1] <= $indent->[-2] ) {
		die \"YAML::Tiny found bad indenting in line '$lines->[0]'";
	}

	# Pull the lines
	my @multiline = ();
	while ( @$lines ) {
		$lines->[0] =~ /^(\s*)/;
		last unless length($1) >= $indent->[-1];
		push @multiline, substr(shift(@$lines), length($1));
	}

	my $j = (substr($string, 0, 1) eq '>') ? ' ' : "\n";
	my $t = (substr($string, 1, 1) eq '-') ? ''  : "\n";
	return join( $j, @multiline ) . $t;
}

# Parse an array
sub _read_array {
	my ($self, $array, $indent, $lines) = @_;

	while ( @$lines ) {
		# Check for a new document
		if ( $lines->[0] =~ /^(?:---|\.\.\.)/ ) {
			while ( @$lines and $lines->[0] !~ /^---/ ) {
				shift @$lines;
			}
			return 1;
		}

		# Check the indent level
		$lines->[0] =~ /^(\s*)/;
		if ( length($1) < $indent->[-1] ) {
			return 1;
		} elsif ( length($1) > $indent->[-1] ) {
			die \"YAML::Tiny found bad indenting in line '$lines->[0]'";
		}

		if ( $lines->[0] =~ /^(\s*\-\s+)[^\'\"]\S*\s*:(?:\s+|$)/ ) {
			# Inline nested hash
			my $indent2 = length("$1");
			$lines->[0] =~ s/-/ /;
			push @$array, { };
			$self->_read_hash( $array->[-1], [ @$indent, $indent2 ], $lines );

		} elsif ( $lines->[0] =~ /^\s*\-(\s*)(.+?)\s*\z/ ) {
			# Array entry with a value
			shift @$lines;
			push @$array, $self->_read_scalar( "$2", [ @$indent, undef ], $lines );

		} elsif ( $lines->[0] =~ /^\s*\-\s*\z/ ) {
			shift @$lines;
			unless ( @$lines ) {
				push @$array, undef;
				return 1;
			}
			if ( $lines->[0] =~ /^(\s*)\-/ ) {
				my $indent2 = length("$1");
				if ( $indent->[-1] == $indent2 ) {
					# Null array entry
					push @$array, undef;
				} else {
					# Naked indenter
					push @$array, [ ];
					$self->_read_array( $array->[-1], [ @$indent, $indent2 ], $lines );
				}

			} elsif ( $lines->[0] =~ /^(\s*)\S/ ) {
				push @$array, { };
				$self->_read_hash( $array->[-1], [ @$indent, length("$1") ], $lines );

			} else {
				die \"YAML::Tiny failed to classify line '$lines->[0]'";
			}

		} elsif ( defined $indent->[-2] and $indent->[-1] == $indent->[-2] ) {
			# This is probably a structure like the following...
			# ---
			# foo:
			# - list
			# bar: value
			#
			# ... so lets return and let the hash parser handle it
			return 1;

		} else {
			die \"YAML::Tiny failed to classify line '$lines->[0]'";
		}
	}

	return 1;
}

# Parse an array
sub _read_hash {
	my ($self, $hash, $indent, $lines) = @_;

	while ( @$lines ) {
		# Check for a new document
		if ( $lines->[0] =~ /^(?:---|\.\.\.)/ ) {
			while ( @$lines and $lines->[0] !~ /^---/ ) {
				shift @$lines;
			}
			return 1;
		}

		# Check the indent level
		$lines->[0] =~ /^(\s*)/;
		if ( length($1) < $indent->[-1] ) {
			return 1;
		} elsif ( length($1) > $indent->[-1] ) {
			die \"YAML::Tiny found bad indenting in line '$lines->[0]'";
		}

		# Get the key
		unless ( $lines->[0] =~ s/^\s*([^\'\" ][^\n]*?)\s*:(\s+(?:\#.*)?|$)// ) {
			if ( $lines->[0] =~ /^\s*[?\'\"]/ ) {
				die \"YAML::Tiny does not support a feature in line '$lines->[0]'";
			}
			die \"YAML::Tiny failed to classify line '$lines->[0]'";
		}
		my $key = $1;

		# Do we have a value?
		if ( length $lines->[0] ) {
			# Yes
			$hash->{$key} = $self->_read_scalar( shift(@$lines), [ @$indent, undef ], $lines );
		} else {
			# An indent
			shift @$lines;
			unless ( @$lines ) {
				$hash->{$key} = undef;
				return 1;
			}
			if ( $lines->[0] =~ /^(\s*)-/ ) {
				$hash->{$key} = [];
				$self->_read_array( $hash->{$key}, [ @$indent, length($1) ], $lines );
			} elsif ( $lines->[0] =~ /^(\s*)./ ) {
				my $indent2 = length("$1");
				if ( $indent->[-1] >= $indent2 ) {
					# Null hash entry
					$hash->{$key} = undef;
				} else {
					$hash->{$key} = {};
					$self->_read_hash( $hash->{$key}, [ @$indent, length($1) ], $lines );
				}
			}
		}
	}

	return 1;
}

# Save an object to a file
sub write {
	my $self = shift;
	my $file = shift or return $self->_error('No file name provided');

	# Write it to the file
	open( CFG, '>' . $file ) or return $self->_error(
		"Failed to open file '$file' for writing: $!"
		);
	print CFG $self->write_string;
	close CFG;

	return 1;
}

# Save an object to a string
sub write_string {
	my $self = shift;
	return '' unless @$self;

	# Iterate over the documents
	my $indent = 0;
	my @lines  = ();
	foreach my $cursor ( @$self ) {
		push @lines, '---';

		# An empty document
		if ( ! defined $cursor ) {
			# Do nothing

		# A scalar document
		} elsif ( ! ref $cursor ) {
			$lines[-1] .= ' ' . $self->_write_scalar( $cursor, $indent );

		# A list at the root
		} elsif ( ref $cursor eq 'ARRAY' ) {
			unless ( @$cursor ) {
				$lines[-1] .= ' []';
				next;
			}
			push @lines, $self->_write_array( $cursor, $indent, {} );

		# A hash at the root
		} elsif ( ref $cursor eq 'HASH' ) {
			unless ( %$cursor ) {
				$lines[-1] .= ' {}';
				next;
			}
			push @lines, $self->_write_hash( $cursor, $indent, {} );

		} else {
			Carp::croak("Cannot serialize " . ref($cursor));
		}
	}

	join '', map { "$_\n" } @lines;
}

sub _write_scalar {
	my $string = $_[1];
	return '~'  unless defined $string;
	return "''" unless length  $string;
	if ( $string =~ /[\x00-\x08\x0b-\x0d\x0e-\x1f\"\'\n]/ ) {
		$string =~ s/\\/\\\\/g;
		$string =~ s/"/\\"/g;
		$string =~ s/\n/\\n/g;
		$string =~ s/([\x00-\x1f])/\\$UNPRINTABLE[ord($1)]/g;
		return qq|"$string"|;
	}
	if ( $string =~ /(?:^\W|\s|:\z)/ or $QUOTE{$string} ) {
		return "'$string'";
	}
	return $string;
}

sub _write_array {
	my ($self, $array, $indent, $seen) = @_;
	if ( $seen->{refaddr($array)}++ ) {
		die "YAML::Tiny does not support circular references";
	}
	my @lines  = ();
	foreach my $el ( @$array ) {
		my $line = ('  ' x $indent) . '-';
		my $type = ref $el;
		if ( ! $type ) {
			$line .= ' ' . $self->_write_scalar( $el, $indent + 1 );
			push @lines, $line;

		} elsif ( $type eq 'ARRAY' ) {
			if ( @$el ) {
				push @lines, $line;
				push @lines, $self->_write_array( $el, $indent + 1, $seen );
			} else {
				$line .= ' []';
				push @lines, $line;
			}

		} elsif ( $type eq 'HASH' ) {
			if ( keys %$el ) {
				push @lines, $line;
				push @lines, $self->_write_hash( $el, $indent + 1, $seen );
			} else {
				$line .= ' {}';
				push @lines, $line;
			}

		} else {
			die "YAML::Tiny does not support $type references";
		}
	}

	@lines;
}

sub _write_hash {
	my ($self, $hash, $indent, $seen) = @_;
	if ( $seen->{refaddr($hash)}++ ) {
		die "YAML::Tiny does not support circular references";
	}
	my @lines  = ();
	foreach my $name ( sort keys %$hash ) {
		my $el   = $hash->{$name};
		my $line = ('  ' x $indent) . "$name:";
		my $type = ref $el;
		if ( ! $type ) {
			$line .= ' ' . $self->_write_scalar( $el, $indent + 1 );
			push @lines, $line;

		} elsif ( $type eq 'ARRAY' ) {
			if ( @$el ) {
				push @lines, $line;
				push @lines, $self->_write_array( $el, $indent + 1, $seen );
			} else {
				$line .= ' []';
				push @lines, $line;
			}

		} elsif ( $type eq 'HASH' ) {
			if ( keys %$el ) {
				push @lines, $line;
				push @lines, $self->_write_hash( $el, $indent + 1, $seen );
			} else {
				$line .= ' {}';
				push @lines, $line;
			}

		} else {
			die "YAML::Tiny does not support $type references";
		}
	}

	@lines;
}

# Set error
sub _error {
	$YAML::Tiny::errstr = $_[1];
	undef;
}

# Retrieve error
sub errstr {
	$YAML::Tiny::errstr;
}





#####################################################################
# YAML Compatibility

sub Dump {
	YAML::Tiny->new(@_)->write_string;
}

sub Load {
	my $self = YAML::Tiny->read_string(@_);
	unless ( $self ) {
		Carp::croak("Failed to load YAML document from string");
	}
	if ( wantarray ) {
		return @$self;
	} else {
		# To match YAML.pm, return the last document
		return $self->[-1];
	}
}

BEGIN {
	*freeze = *Dump;
	*thaw   = *Load;
}

sub DumpFile {
	my $file = shift;
	YAML::Tiny->new(@_)->write($file);
}

sub LoadFile {
	my $self = YAML::Tiny->read($_[0]);
	unless ( $self ) {
		Carp::croak("Failed to load YAML document from '" . ($_[0] || '') . "'");
	}
	if ( wantarray ) {
		return @$self;
	} else {
		# Return only the last document to match YAML.pm, 
		return $self->[-1];
	}
}





#####################################################################
# Use Scalar::Util if possible, otherwise emulate it

BEGIN {
	local $@;
	eval {
		require Scalar::Util;
	};
	my $v = eval("$Scalar::Util::VERSION") || 0;
	if ( $@ or $v < 1.18 ) {
		eval <<'END_PERL';
# Scalar::Util failed to load or too old
sub refaddr {
	my $pkg = ref($_[0]) or return undef;
	if ( !! UNIVERSAL::can($_[0], 'can') ) {
		bless $_[0], 'Scalar::Util::Fake';
	} else {
		$pkg = undef;
	}
	"$_[0]" =~ /0x(\w+)/;
	my $i = do { local $^W; hex $1 };
	bless $_[0], $pkg if defined $pkg;
	$i;
}
END_PERL
	} else {
		*refaddr = *Scalar::Util::refaddr;
	}
}

1;

__END__

=pod

=head1 NAME

YAML::Tiny - Read/Write YAML files with as little code as possible

=head1 PREAMBLE

The YAML specification is huge. Really, B<really> huge. It contains all the
functionality of XML, except with flexibility and choice, which makes it
easier to read, but with a formal specification that is more complex than
XML.

The original pure-Perl implementation L<YAML> costs just over 4 megabytes
of memory to load. Just like with Windows .ini files (3 meg to load) and
CSS (3.5 meg to load) the situation is just asking for a B<YAML::Tiny>
module, an incomplete but correct and usable subset of the functionality,
in as little code as possible.

Like the other C<::Tiny> modules, YAML::Tiny has no non-core dependencies,
does not require a compiler to install, is back-compatible to Perl 5.004,
and can be inlined into other modules if needed.

In exchange for this adding this extreme flexibility, it provides support
for only a limited subset of YAML. But the subset supported contains most
of the features for the more common usese of YAML.

=head1 SYNOPSIS

    #############################################
    # In your file
    
    ---
    rootproperty: blah
    section:
      one: two
      three: four
      Foo: Bar
      empty: ~
    
    
    
    #############################################
    # In your program
    
    use YAML::Tiny;
    
    # Create a YAML file
    my $yaml = YAML::Tiny->new;
    
    # Open the config
    $yaml = YAML::Tiny->read( 'file.yml' );
    
    # Reading properties
    my $root = $yaml->[0]->{rootproperty};
    my $one  = $yaml->[0]->{section}->{one};
    my $Foo  = $yaml->[0]->{section}->{Foo};
    
    # Changing data
    $yaml->[0]->{newsection} = { this => 'that' }; # Add a section
    $yaml->[0]->{section}->{Foo} = 'Not Bar!';     # Change a value
    delete $yaml->[0]->{section};                  # Delete a value
    
    # Add an entire document
    $yaml->[1] = [ 'foo', 'bar', 'baz' ];
    
    # Save the file
    $yaml->write( 'file.conf' );

=head1 DESCRIPTION

B<YAML::Tiny> is a perl class for reading and writing YAML-style files,
written with as little code as possible, reducing load time and memory
overhead.

Most of the time it is accepted that Perl applications use a lot
of memory and modules. The B<::Tiny> family of modules is specifically
intended to provide an ultralight and zero-dependency alternative to
many more-thorough standard modules.

This module is primarily for reading human-written files (like simple
config files) and generating very simple human-readable files. Note that
I said B<human-readable> and not B<geek-readable>. The sort of files that
your average manager or secretary should be able to look at and make
sense of.

L<YAML::Tiny> does not generate comments, it won't necesarily preserve the
order of your hashes, and it will normalise if reading in and writing out
again.

It only supports a very basic subset of the full YAML specification.

Usage is targetted at files like Perl's META.yml, for which a small and
easily-embeddable module is extremely attractive.

Features will only be added if they are human readable, and can be written
in a few lines of code. Please don't be offended if your request is
refused. Someone has to draw the line, and for YAML::Tiny that someone
is me.

If you need something with more power move up to L<YAML> (4 megabytes of
memory overhead) or L<YAML::Syck> (275k, but requires libsyck and a C
compiler).

To restate, L<YAML::Tiny> does B<not> preserve your comments, whitespace,
or the order of your YAML data. But it should round-trip from Perl
structure to file and back again just fine.

=head1 YAML TINY SPECIFICATION

This section of the documentation provides a specification for "YAML Tiny",
a subset of the YAML specification.

It is based on and described comparatively to the YAML 1.1 Working Draft
2004-12-28 specification, located at L<http://yaml.org/spec/current.html>.

Terminology and chapter numbers are based on that specification.

=head2 1. Introduction and Goals

The purpose of the YAML Tiny specification is to describe a useful subset
of the YAML specification that can be used for typical document-oriented
use cases such as configuration files and simple data structure dumps.

Many specification elements that add flexibility or extensibility are
intentionally removed, as is support for complex datastructures, class
and object-orientation.

In general, the YAML Tiny language targets only those data structures
available in JSON, with the additional limitation that only simple keys
are supported.

As a result, all possible YAML Tiny documents should be able to be
transformed into an equivalent JSON document, although the reverse is
not necesarily true (but will be true in simple cases).

As a result of these simplifications the YAML Tiny specification should
be implementable in a (relatively) small amount of code in any language
that supports Perl Compatible Regular Expressions (PCRE).

=head2 2. Introduction

YAML Tiny supports three data structures. These are scalars (in a variety
of forms), block-form sequences and block-form mappings. Flow-style
sequences and mappings are not supported, with some minor exceptions
detailed later.

The use of three dashes "---" to indicate the start of a new document is
supported, and multiple documents per file/stream is allowed.

Both line and inline comments are supported.

Scalars are supported via the plain style, single quote and double quote,
as well as literal-style and folded-style multi-line scalars.

The use of explicit tags is not supported.

The use of "null" type scalars is supported via the ~ character.

The use of "bool" type scalars is not supported.

However, serializer implementations should take care to explicitly escape
strings that match a "bool" keyword in the following set to prevent other
implementations that do support "bool" accidentally reading a string as a
boolean

  y|Y|yes|Yes|YES|n|N|no|No|NO
  |true|True|TRUE|false|False|FALSE
  |on|On|ON|off|Off|OFF

The use of anchors and aliases is not supported.

The use of directives is supported only for the %YAML directive.

=head2 3. Processing YAML Tiny Information

B<Processes>

The YAML specification dictates three-phase serialization and three-phase
deserialization.

The YAML Tiny specification does not mandate any particular methodology
or mechanism for parsing.

Any compliant parser is only required to parse a single document at a
time. The ability to support streaming documents is optional and most
likely non-typical.

Because anchors and aliases are not supported, the resulting representation
graph is thus directed but (unlike the main YAML specification) B<acyclic>.

Circular references/pointers are not possible, and any YAML Tiny serializer
detecting a circular reference should error with an appropriate message.

B<Presentation Stream>

YAML Tiny is notionally unicode, but support for unicode is required if the
underlying language or system being used to implement a parser does not
support Unicode. If unicode is encountered in this case an error should be
returned.

B<Loading Failure Points>

YAML Tiny parsers and emitters are not expected to recover from adapt to
errors. The specific error modality of any implementation is not dictated
(return codes, exceptions, etc) but is expected to be consistant.

=head2 4. Syntax

B<Character Set>

YAML Tiny streams are implemented primarily using the ASCII character set,
although the use of Unicode inside strings is allowed if support by the
implementation.

Specific YAML Tiny encoded document types aiming for maximum compatibility
should restrict themselves to ASCII.

The escaping and unescaping of the 8-bit YAML escapes is required.

The escaping and unescaping of 16-bit and 32-bit YAML escapes is not
required.

B<Indicator Characters>

Support for the "~" null/undefined indicator is required.

Implementations may represent this as appropriate for the underlying
language.

Support for the "-" block sequence indicator is required.

Support for the "?" mapping key indicator is B<not> required.

Support for the ":" mapping value indicator is required.

Support for the "," flow collection indicator is B<not> required.

Support for the "[" flow sequence indicator is B<not> required, with
one exception (detailed below).

Support for the "]" flow sequence indicator is B<not> required, with
one exception (detailed below).

Support for the "{" flow mapping indicator is B<not> required, with
one exception (detailed below).

Support for the "}" flow mapping indicator is B<not> required, with
one exception (detailed below).

Support for the "#" comment indicator is required.

Support for the "&" anchor indicator is B<not> required.

Support for the "*" alias indicator is B<not> required.

Support for the "!" tag indicator is B<not> required.

Support for the "|" literal block indicator is required.

Support for the ">" folded block indicator is required.

Support for the "'" single quote indicator is required.

Support for the """ double quote indicator is required.

Support for the "%" directive indicator is required, but only
for the special case of a %YAML version directive before the
"---" document header, or on the same line as the document header.

For example:

  %YAML 1.1
  ---
  - A sequence with a single element

Special Exception:

To provide the ability to support empty sequences
and mappings, support for the constructs [] (empty sequence) and {}
(empty mapping) are required.

For example, 
  
  %YAML 1.1
  # A document consisting of only an empty mapping
  --- {}
  # A document consisting of only an empty sequence
  --- []
  # A document consisting of an empty mapping within a sequence
  - foo
  - {}
  - bar

B<Syntax Primitives>

Other than the empty sequence and mapping cases described above, YAML Tiny
supports only the indentation-based block-style group of contexts.

All five scalar contexts are supported.

Indentation spaces work as per the YAML specification in all cases.

Comments work as per the YAML specification in all simple cases.
Support for indented multi-line comments is B<not> required.

Seperation spaces work as per the YAML specification in all cases.

B<YAML Tiny Character Stream>

The only directive supported by the YAML Tiny specification is the
%YAML language/version identifier. Although detected, this directive
will have no control over the parsing itself.

The parser must recognise both the YAML 1.0 and YAML 1.1+ formatting
of this directive (as well as the commented form, although no explicit
code should be needed to deal with this case, being a comment anyway)

That is, all of the following should be supported.

  --- #YAML:1.0
  - foo

  %YAML:1.0
  ---
  - foo

  % YAML 1.1
  ---
  - foo

Support for the %TAG directive is B<not> required.

Support for additional directives is B<not> required.

Support for the document boundary marker "---" is required.

Support for the document boundary market "..." is B<not> required.

If necesary, a document boundary should simply by indicated with a
"---" marker, with not preceding "..." marker.

Support for empty streams (containing no documents) is required.

Support for implicit document starts is required.

That is, the following must be equivalent.

 # Full form
 %YAML 1.1
 ---
 foo: bar

 # Implicit form
 foo: bar

B<Nodes>

Support for nodes optional anchor and tag properties are B<not> required.

Support for node anchors is B<not> required.

Support for node tags is B<not> required.

Support for alias nodes is B<not> required.

Support for flow nodes is B<not> required.

Support for block nodes is required.

B<Scalar Styles>

Support for all five scalar styles are required as per the YAML
specification, although support for quoted scalars spanning more
than one line is B<not> required.

Support for the chomping indicators on multi-line scalar styles
is required.

B<Collection Styles>

Support for block-style sequences is required.

Support for flow-style sequences is B<not> required.

Support for block-style mappings is required.

Support for flow-style mappings is B<not> required.

Both sequences and mappings should be able to be arbitrarily
nested.

Support for plain-style mapping keys is required.

Support for quoted keys in mappings is B<not> required.

Support for "?"-indicated explicit keys is B<not> required.

Here endeth the specification.

=head2 Additional Perl-Specific Notes

For some Perl applications, it's important to know if you really have a
number and not a string.

That is, in some contexts is important that 3 the number is distinctive
from "3" the string.

Because even Perl itself is not trivially able to understand the difference
(certainly without XS-based modules) Perl implementations of the YAML Tiny
specification are not required to retain the distinctiveness of 3 vs "3".

=head1 METHODS

=head2 new

The constructor C<new> creates and returns an empty C<YAML::Tiny> object.

=head2 read $filename

The C<read> constructor reads a YAML file from a file name,
and returns a new C<YAML::Tiny> object containing the parsed content.

Returns the object on success, or C<undef> on error.

When C<read> fails, C<YAML::Tiny> sets an error message internally
you can recover via C<YAML::Tiny-E<gt>errstr>. Although in B<some>
cases a failed C<read> will also set the operating system error
variable C<$!>, not all errors do and you should not rely on using
the C<$!> variable.

=head2 read_string $string;

The C<read> constructor reads a YAML file from a file name,
and returns a new C<YAML::Tiny> object containing the parsed content.

Returns the object on success, or C<undef> on error.

=head2 write $filename

The C<write> method generates the file content for the properties, and
writes it to disk to the filename specified.

Returns true on success or C<undef> on error.

=head2 write_string

Generates the file content for the object and returns it as a string.

=head2 errstr

When an error occurs, you can retrieve the error message either from the
C<$YAML::Tiny::errstr> variable, or using the C<errstr()> method.

=head1 FUNCTIONS

YAML::Tiny implements a number of functions to add compatibility with
the L<YAML> API. These should be a drop-in replacement, except that
YAML::Tiny will B<not> export functions by default, and so you will need
to explicitly import the functions.

=head2 Dump

  my $string = Dump(list-of-Perl-data-structures);

Turn Perl data into YAML. This function works very much like
Data::Dumper::Dumper().

It takes a list of Perl data strucures and dumps them into a serialized
form.

It returns a string containing the YAML stream.

The structures can be references or plain scalars.

=head2 Load

  my @documents = Load(string-containing-a-YAML-stream);

Turn YAML into Perl data. This is the opposite of Dump.

Just like L<Storable>'s thaw() function or the eval() function in relation
to L<Data::Dumper>.

It parses a string containing a valid YAML stream into a list of Perl data
structures.

=head2 freeze() and thaw()

Aliases to Dump() and Load() for L<Storable> fans. This will also allow
YAML::Tiny to be plugged directly into modules like POE.pm, that use the
freeze/thaw API for internal serialization.

=head2 DumpFile(filepath, list)

Writes the YAML stream to a file instead of just returning a string.

=head2 LoadFile(filepath)

Reads the YAML stream from a file instead of a string.

=head1 SUPPORT

Bugs should be reported via the CPAN bug tracker at

L<http://rt.cpan.org/NoAuth/ReportBug.html?Queue=YAML-Tiny>

=begin html

For other issues, or commercial enhancement or support, please contact
<a href="http://ali.as/">Adam Kennedy</a> directly.

=end html

=head1 AUTHOR

Adam Kennedy E<lt>adamk@cpan.orgE<gt>

=head1 SEE ALSO

L<YAML>, L<YAML::Syck>, L<Config::Tiny>, L<CSS::Tiny>,
L<http://use.perl.org/~Alias/journal/29427>, L<http://ali.as/>

=head1 COPYRIGHT

Copyright 2006 - 2012 Adam Kennedy.

This program is free software; you can redistribute
it and/or modify it under the same terms as Perl itself.

The full text of the license can be found in the
LICENSE file included with this module.

=cut
