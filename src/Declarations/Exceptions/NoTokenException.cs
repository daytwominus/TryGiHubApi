using System;

namespace Declarations.Exceptions
{
    public class NoTokenException : Exception
    {
		public NoTokenException(string e) : base(e) { }
	}
}
