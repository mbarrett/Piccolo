using System.Net;
using System.Net.Http;

namespace Piccolo
{
	public class Response
	{
		public class Error
		{
			public static HttpResponseMessage<TOutput> NotFound<TOutput>()
			{
				var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
				return new HttpResponseMessage<TOutput>(responseMessage);
			}
		}

		public class Success
		{
			public static HttpResponseMessage<TOutput> Ok<TOutput>(TOutput content)
			{
				var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) {Content = new ObjectContent(content)};
				return new HttpResponseMessage<TOutput>(responseMessage);
			}

			public static HttpResponseMessage<TOutput> Created<TOutput>(TOutput content)
			{
				var responseMessage = new HttpResponseMessage(HttpStatusCode.Created) {Content = new ObjectContent(content)};
				return new HttpResponseMessage<TOutput>(responseMessage);
			}

			public static HttpResponseMessage<dynamic> NoContent()
			{
				var responseMessage = new HttpResponseMessage(HttpStatusCode.NoContent);
				return new HttpResponseMessage<dynamic>(responseMessage);
			}
		}
	}
}