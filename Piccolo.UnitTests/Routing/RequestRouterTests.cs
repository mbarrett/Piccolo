﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using NUnit.Framework;
using Piccolo.Routing;
using Shouldly;

namespace Piccolo.UnitTests.Routing
{
	public class RequestRouterTests
	{
		[TestFixture]
		public class when_searching_for_request_handler_that_matches_root : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(RootRequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_static_level1_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/level-1", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(StaticLevel1RequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_static_level2_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/level-1/level-2", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(StaticLevel2RequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_alternative_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/alternative-path", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(StaticLevel2RequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_dynamic_level1_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/DynamicLevel1/1", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(DynamicLevel1RequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_dynamic_level2_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/level-1/2", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(DynamicLevel2RequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_dynamic_multi_level_path : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/1/2/3", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_request_handler()
			{
				_routeHandlerLookupResult.RequestHandlerType.ShouldBe(typeof(DynamicMultiLevelRequestHandler));
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler_that_matches_dynamic_multi_level_path_that_is_not_routed : given_request_router_initialised_with_test_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler("get", new Uri("http://test.com/1/2/3/is-not-routed", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_failed_result()
			{
				_routeHandlerLookupResult.ShouldBe(RouteHandlerLookupResult.FailedResult);
			}
		}

		[TestFixture]
		public class when_searching_for_request_handler : given_request_router_lookup_initialised_with_0_routes
		{
			private RouteHandlerLookupResult _routeHandlerLookupResult;

			[SetUp]
			public void SetUp()
			{
				_routeHandlerLookupResult = RequestRouter.FindRequestHandler(string.Empty, new Uri("http://test.com/", UriKind.Absolute));
			}

			[Test]
			public void it_should_return_failed_result()
			{
				_routeHandlerLookupResult.ShouldBe(RouteHandlerLookupResult.FailedResult);
			}
		}

		public abstract class given_request_router_initialised_with_test_routes : given_request_router
		{
			private static readonly IEnumerable<Type> _testRoutes = new List<Type>
			{
				typeof(RootRequestHandler),
				typeof(StaticLevel1RequestHandler),
				typeof(StaticLevel2RequestHandler),
				typeof(DynamicLevel1RequestHandler),
				typeof(DynamicLevel2RequestHandler),
				typeof(DynamicMultiLevelRequestHandler)
			};

			protected given_request_router_initialised_with_test_routes() : base(_testRoutes)
			{
			}
		}

		public abstract class given_request_router_lookup_initialised_with_0_routes : given_request_router
		{
			protected given_request_router_lookup_initialised_with_0_routes() : base(new List<Type>())
			{
			}
		}

		public abstract class given_request_router
		{
			protected RequestRouter RequestRouter;

			protected given_request_router(IEnumerable<Type> requestHandlers)
			{
				RequestRouter = new RequestRouter(requestHandlers);
			}
		}

		#region Test Classes

		[Route("/")]
		public class RootRequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}
		}

		[Route("/level-1")]
		public class StaticLevel1RequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}
		}

		[Route("/level-1/level-2")]
		[Route("/alternative-path")]
		public class StaticLevel2RequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}
		}

		[Route("/DynamicLevel1/{Value}")]
		public class DynamicLevel1RequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}

			public Int32 Value { get; set; }
		}

		[Route("/level-1/{DynamicLevel2}")]
		public class DynamicLevel2RequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}

			public int DynamicLevel2 { get; set; }
		}

		[Route("/{DynamicLevel1}/{DynamicLevel2}/{DynamicLevel3}")]
		public class DynamicMultiLevelRequestHandler : IGet<string>
		{
			[ExcludeFromCodeCoverage]
			public HttpResponseMessage<string> Get()
			{
				return new HttpResponseMessage<string>(new HttpResponseMessage());
			}

			public int DynamicLevel1 { get; set; }
			public int DynamicLevel2 { get; set; }
			public int DynamicLevel3 { get; set; }
		}

		#endregion
	}
}