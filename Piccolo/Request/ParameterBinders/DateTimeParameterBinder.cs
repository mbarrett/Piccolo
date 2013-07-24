using System;
using System.Reflection;

namespace Piccolo.Request.ParameterBinders
{
	public class DateTimeParameterBinder : IRouteParameterBinder
	{
		public void BindRouteParameter(IRequestHandler requestHandler, PropertyInfo property, string rawValue)
		{
			property.SetValue(requestHandler, DateTime.Parse(rawValue), null);
		}
	}
}