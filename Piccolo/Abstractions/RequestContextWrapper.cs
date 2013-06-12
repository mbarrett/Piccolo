using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Piccolo.Abstractions
{
	[ExcludeFromCodeCoverage]
	public class RequestContextWrapper : IRequestContextWrapper
	{
		private readonly HttpContext _context;

		public RequestContextWrapper(HttpContext context)
		{
			_context = context;
		}

		public Uri Uri
		{
			get { return _context.Request.Url; }
		}
	}
}