using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Piccolo.Request
{
	[ExcludeFromCodeCoverage]
	public class RequestContextWrapper : IRequestContextWrapper
	{
		private readonly HttpContext _context;

		public RequestContextWrapper(HttpContext context)
		{
			_context = context;
		}

		public string Verb
		{
			get { return _context.Request.HttpMethod; }
		}

		public Uri Uri
		{
			get { return _context.Request.Url; }
		}

		public string Payload
		{
			get { return _context.Request.Form.Count > 0 ? _context.Request.Form[0] : string.Empty; }
		}
	}
}