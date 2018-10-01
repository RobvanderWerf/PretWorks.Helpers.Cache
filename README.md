## Welcome to PretWorks.Helpers.Cache

Easy to use helper for your basic caching needs. 
It's a wrapper around the IMemoryCache that will help with the basic tasks of null checking.

## Basic installation

Getting the package:

    Install-Package PretWorks.Helpers.Cache

## Setup:

Register the cache helper in startup.cs

    services.AddTransient(typeof(ICachehelper), typeof(InMemoryCacheHelper));
    
Register the cache provider in startup.cs

(for example the MemoryCache from the nuget package Microsoft.Extensions.Caching.Memory)

    services.AddMemoryCache();

## Basic usage:
You can now inject the ICacheService into your code and use it for example:

	 public class TestService
	 { 
	 
		private readonly ICachehelper _cachehelper;
		private readonly IHttpClientFactory _clientFactory;
			
		public TestService(ICachehelper cachehelper, IHttpClientFactory clientFactory)
		{
			_cachehelper = cachehelper;
			_clientFactory = clientFactory;
		}

		private async Task<string> GetAsync()
		{
			return await _cachehelper.GetOrAddAsync(
			"MyCacheKey",  // Cache Key
			async () =>
			{
				var client = _clientFactory.CreateClient("Get");

				var response = await client.GetAsync(new Uri("https://www.google.com"));
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsStringAsync();
				}

				return null;
			}, // Call to get data
			300 // Time in seconds to cache
			);
		}
	}
