// PotDesc.exe generates one or more magic potion descriptions.
// Copyright (c) 2023 Timothy J. Bruce

/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Icod.Helpers;
using System.Runtime.ExceptionServices;

namespace Dnd.PotionDescription {

	public static class Program {

		#region nested classes
		private sealed class Pair<T> {
			private static readonly System.Int32 theHashCode;
			private readonly T myFirst;
			private readonly T mySecond;
			private readonly System.Int32 myHashCode;
			static Pair() {
				theHashCode = System.Reflection.Assembly.GetExecutingAssembly().GetHashCode();
			}
			public Pair( T first, T second ) {
				myFirst = first;
				mySecond = second;
				myHashCode = theHashCode;
				unchecked {
					if ( first is not null ) {
						myHashCode += first.GetHashCode();
					}
					if ( second is not null ) {
						myHashCode += second.GetHashCode();
					}
				}
			}
			public T First {
				get {
					return myFirst;
				}
			}
			public T Second {
				get {
					return mySecond;
				}
			}
			public sealed override System.Int32 GetHashCode() {
				return myHashCode;
			}
			public sealed override System.Boolean Equals( System.Object? obj ) {
				if ( obj is null ) {
					return false;
				} else if ( obj is Pair<T> ) {
					return this.Equals( obj as Pair<T> );
				} else {
					return false;
				}
			}
			public System.Boolean Equals( Pair<T>? other ) {
				if ( other is null ) {
					return false;
				} else {
					return System.Object.Equals( myFirst, other.First ) && System.Object.Equals( mySecond, other.Second );
				}
			}
			public static System.Boolean operator ==( Pair<T>? first, Pair<T>? second ) {
				if ( ( first is null ) && ( second is null ) ) {
					return true;
				} else if ( ( first is null ) || ( second is null ) ) {
					return false;
				} else {
					return first.Equals( second );
				}
			}
			public static System.Boolean operator !=( Pair<T>? first, Pair<T>? second ) {
				return !( first == second );
			}
		}
		#endregion nested classes


		#region fields
		private const System.Int32 theDefaultNumber = 10;

		private static readonly System.Collections.Generic.IList<System.String> theAppearance;

		private static readonly System.Collections.Generic.IList<System.String> theOpacity;

		private static readonly System.Collections.Generic.IList<System.String> theAmbianceAmplitude;
		private static readonly System.Collections.Generic.IList<System.String> theAmbiance;
		private static readonly System.Collections.Generic.IList<System.String> theTaste;
		private static readonly System.Collections.Generic.IList<System.String> theOdor;

		private static readonly System.Collections.Generic.IList<System.String> theBlack;
		private static readonly System.Collections.Generic.IList<System.String> theGrey;
		private static readonly System.Collections.Generic.IList<System.String> theWhite;
		private static readonly System.Collections.Generic.IList<System.String> theRed;
		private static readonly System.Collections.Generic.IList<System.String> theOrange;
		private static readonly System.Collections.Generic.IList<System.String> theYellow;
		private static readonly System.Collections.Generic.IList<System.String> theGreen;
		private static readonly System.Collections.Generic.IList<System.String> theBlue;
		private static readonly System.Collections.Generic.IList<System.String> theViolet;
		private static readonly System.Collections.Generic.IList<System.String> theBrown;
		private static readonly System.Collections.Generic.IList<System.String> theMetalic;
		private static readonly System.Collections.Generic.IList<System.Collections.Generic.IList<System.String>> theColors;
		#endregion fields


		#region .ctor
		static Program() {
			theAppearance = new System.Collections.Generic.List<System.String> {
				"astringent", "bubbling", "cloudy", "effervescent",
				"fuming", "inert", "oily", "slick", "smoky", "sticky", 
				"syrupy", "vaporous", "viscous", "watery"
			}.AsReadOnly();

			theOpacity = new System.Collections.Generic.List<System.String> {
				"clear", "rainbowed",
				"flecked", "luminous", "muted", "opaline", "opaque", "phosphorescent", "translucent",
				"layered", "polka-dotted", "ribboned", "striped",
				"variegated",
			}.AsReadOnly();

			theAmbianceAmplitude = new System.Collections.Generic.List<System.String> {
				"bland", "decidedly", "delicate", "disagreeable", "distinct", 
				"faint", "harsh", "mellow", "mild", "moderate", "noisome", 
				"noticeable", "noxious", "severe", "sharp", "strong" 
			}.AsReadOnly();

			theAmbiance = new System.Collections.Generic.List<System.String> {
				"acidic", "bilious", "bitter", "burning",
				"buttery", "dusty", "earthy", "fiery", "fishy",
				"greasy", "herbal", "honeyed", "lemony", "meaty",
				"metallic", "milky", "musty", "oniony", "peppery",
				"perfumy", "salty", "soothing", "sour",
				"spicy", "sweet", "tart", "vinegary", "watery"
			}.AsReadOnly();
			theTaste = new System.Collections.Generic.List<System.String>( theAmbiance ).AsReadOnly();
			theOdor = new System.Collections.Generic.List<System.String>( theAmbiance ).AsReadOnly();

			theBlack = new System.Collections.Generic.List<System.String> {
				"ebony", "inky", "pitchy", "sable", "sooty"
			}.AsReadOnly();
			theGrey = new System.Collections.Generic.List<System.String> {
				"dove", "dun", "neutral grey"
			}.AsReadOnly();
			theWhite = new System.Collections.Generic.List<System.String> {
				"bone", "ivory", "pearl"
			}.AsReadOnly();
			theRed = new System.Collections.Generic.List<System.String> {
				"carmine", "cerise", "cherry", "cinnabar", "coral",
				"crimson", "madder", "maroon", "pink", "rose", "ruby",
				"russet", "rust", "sanguine", "scarlet", "vermilion"
			}.AsReadOnly();
			theOrange = new System.Collections.Generic.List<System.String> {
				"apricot", "flame", "golden", "salmon", "tawny"
			}.AsReadOnly();
			theYellow = new System.Collections.Generic.List<System.String> {
				"amber", "buff", "citrine", "cream", "fallow", "flaxen",
				"ochre", "peach", "saffron", "straw"
			}.AsReadOnly();
			theGreen = new System.Collections.Generic.List<System.String> {
				"aquamarine", "emerald", "olive", "grassy", "leafy"
			}.AsReadOnly();
			theBlue = new System.Collections.Generic.List<System.String> {
				"azure", "beryl", "cerulean", "cobalt", "indigo",
				"navy", "sapphire", "ultramarine"
			}.AsReadOnly();
			theViolet = new System.Collections.Generic.List<System.String> {
				"fuchsia", "heliotrope", "lake", "lavender", "lilac",
				"magento", "mauve", "plum", "puce", "purple"
			}.AsReadOnly();
			theBrown = new System.Collections.Generic.List<System.String> {
				"chocolate", "ecru", "fawn", "mahogany", "tan",
				"terra cotta"
			}.AsReadOnly();
			theMetalic = new System.Collections.Generic.List<System.String> {
				"brassy", "bronze", "coppery", "gold", "silvery", "steely"
			}.AsReadOnly();
			theColors = new System.Collections.Generic.List<System.Collections.Generic.IList<System.String>> {
				theBlack, theGrey, theWhite, theRed, theOrange, theYellow,
				theGreen, theBlue, theViolet, theBrown, theMetalic
			}.AsReadOnly();
		}
		#endregion .ctor


		#region methods
		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			var processor = new Icod.Argh.Processor(
				new Icod.Argh.Definition[] {
					new Icod.Argh.Definition( "help", new System.String[] { "-h", "--help", "/help" } ),
					new Icod.Argh.Definition( "copyright", new System.String[] { "-c", "--copyright", "/copyright" } ),
					new Icod.Argh.Definition( "number", new System.String[] { "-n", "--number", "/number" } ),
					new Icod.Argh.Definition( "output", new System.String[] { "-o", "--output", "/output" } ),
				},
				System.StringComparer.OrdinalIgnoreCase
			);
			processor.Parse( args );

			if ( processor.Contains( "help" ) ) {
				PrintUsage();
				return 1;
			} else if ( processor.Contains( "copyright" ) ) {
				PrintCopyright();
				return 1;
			}

			System.Action<System.String?, System.Collections.Generic.IEnumerable<System.String>> writer;
			if ( processor.TryGetValue( "output", true, out var outputPathName ) ) {
				if ( System.String.IsNullOrEmpty( outputPathName ) ) {
					PrintUsage();
					return 1;
				} else {
					writer = ( a, b ) => a!.WriteLine( b );
				}
			} else {
				writer = ( a, b ) => System.Console.Out.WriteLine( lineEnding: "\r\n", data: b );
			}

			System.Int32 number;
			if ( processor.TryGetValue( "number", true, out var numberString ) ) {
				if ( System.String.IsNullOrEmpty( numberString ) ) {
					number = theDefaultNumber;
				} else {
					if ( !System.Int32.TryParse( numberString, out number ) ) {
						PrintUsage();
						return 1;
					}
					if ( number <= 0 ) {
						PrintUsage();
						return 1;
					}
				}
			} else {
				number = theDefaultNumber;
			}

			System.String[] desc = new System.String[ 5 ];
			var list = new System.Collections.Generic.List<System.String>();
			list.Add( "Appearance,Opacity,Color,Taste,Odor" );
			Pair<System.String> pair;
			var rand = new System.Random();
			for ( var i = 0; i < number; i++ ) {
				desc[ 0 ] = GetAppearance( rand );
				pair = GetOpacityAndColor( rand );
				desc[ 1 ] = pair.First;
				desc[ 2 ] = pair.Second;
				desc[ 3 ] = GetTaste( rand );
				desc[ 4 ] = GetOdor( rand );
				for ( var j = 0; j < 5; j++ ) {
					var d = desc[ j ];
					d = d.Replace( "\"", "\"\"" );
					if (
						d.Contains( ',' )
						|| d.Contains( '"' )
					) {
						d = "\"" + d + "\"";
					}
					desc[ j ] = d;
				}
				list.Add( System.String.Join( ",", desc ) );
			}
			writer( outputPathName, list );

			return 0;
		}

		private static System.String GetAppearance( System.Random rand ) {
			return theAppearance[ rand.Next( theAppearance.Count ) ];
		}
		private static Pair<System.String> GetOpacityAndColor( System.Random rand ) {
			var oi = rand.Next( theOpacity.Count );
			System.String color;
			if ( oi < 2 ) {
				color = System.String.Empty;
			} else if ( oi < 9 ) {
				var n = ( 3 == rand.Next( 4 ) )
					? 2
					: 1
				;
				color = GetColor( rand, n ).EnglishJoin();
			} else if ( oi < 13 ) {
				var n = ( 3 == rand.Next( 4 ) )
					? 3
					: 2
				;
				color = GetColor( rand, n ).EnglishJoin();
			} else {
				var n = rand.Next( 4 ) + 3;
				color = GetColor( rand, n ).EnglishJoin();
			}
			return new Pair<System.String>( theOpacity[ oi ], color );
		}
		private static System.Int32 GetAmbianceAmplitude( System.Random rand ) {
			return rand.Next( theAmbianceAmplitude.Count );
		}
		private static System.String GetTaste( System.Random rand ) {
			var ti = rand.Next( theTaste.Count );
			return ( 0 == rand.Next( 2 ) )
				? theTaste[ ti ]
				: theAmbianceAmplitude[ GetAmbianceAmplitude( rand ) ] + " " + theTaste[ ti ]
			;
		}
		private static System.String GetOdor( System.Random rand ) {
			var oi = rand.Next( theOdor.Count );
			return ( 0 == rand.Next( 2 ) )
				? theOdor[ oi ]
				: theAmbianceAmplitude[ GetAmbianceAmplitude( rand ) ] + " " + theOdor[ oi ]
			;
		}
		private static System.Collections.Generic.IList<System.String> GetColor( System.Random rand, System.Int32 number ) {
			System.Collections.Generic.IList<System.String> output = new System.Collections.Generic.List<System.String>( number );

			var colors = new System.Collections.Generic.List<System.Collections.Generic.List<System.String>>( theColors.Select(
				x => new System.Collections.Generic.List<System.String>( x )
			) );
			var cc = colors.Count;
			for ( var n = 0; n < number; n++ ) {
				var pi = rand.Next( cc );
				var palette = colors[ pi ];
				var pc = palette.Count;
				var ci = rand.Next( pc );
				var cs = palette[ ci ];
				if ( output.Contains( cs ) ) {
					continue;
				}
				output.Add( cs );
				palette.RemoveAt( ci );
				pc = palette.Count;
				if ( 0 == pc ) {
					colors.RemoveAt( pi );
				}
				cc = colors.Count;
				if ( 0 == cc ) {
					break;
				}
			}

			return output;
		}
		private static System.String EnglishJoin( this System.Collections.Generic.IList<System.String> collection ) {
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			var c = collection.Count;
			if ( 1 == c ) {
				output = output.Append( collection.First() );
			} else if ( 2 == c ) {
				output = output.Append( collection[ 0 ] ).Append( " and " ).Append( collection[ 1 ] );
			} else if ( 0 < c ) {
				var stop = c - 1;
				for ( var i = 0; i < stop; i++ ) {
					output = output.Append( collection[ i ] ).Append( ", " );
				}
				output = output.Append( "and " ).Append( collection[ stop ] );
			}
			return output.ToString();
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "PotDesc.exe (-h | --help | /help)" );
			System.Console.Error.WriteLine( "PotDesc.exe (-c | --copyright | /copyright)" );
			System.Console.Error.WriteLine( "PotDesc.exe [(-n | --number | /number) number] [(-o | --output | /output) outputFilePathName]" );
			System.Console.Error.WriteLine( "PotDesc.exe generates one or more magic potion descriptions." );
			System.Console.Error.WriteLine( "number is the number of records to generate." );
			System.Console.Error.WriteLine( "outputFilePathName may be relative or absolute paths." );
			System.Console.Error.WriteLine( "If outputFilePathName is omitted then output is written to StdOut." );
		}

		private static void PrintCopyright() {
			var copy = new System.String[] {
				"PotDesc.exe generates one or more magic potion descriptions.",
				"Copyright (c) 2023 Timothy J. Bruce",
				"",
				"This program is free software: you can redistribute it and / or modify",
				"it under the terms of the GNU General Public License as published by",
				"the Free Software Foundation, either version 3 of the License, or",
				"( at your option ) any later version.",
				"",
				"This program is distributed in the hope that it will be useful,",
				"but WITHOUT ANY WARRANTY; without even the implied warranty of",
				"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the",
				"GNU General Public License for more details.",
				"",
				"You should have received a copy of the GNU General Public License",
				"along with this program.If not, see <https://www.gnu.org/licenses/>."
			};
			foreach ( var line in copy ) {
				System.Console.WriteLine( line );
			}
		}
		#endregion methods

	}

}