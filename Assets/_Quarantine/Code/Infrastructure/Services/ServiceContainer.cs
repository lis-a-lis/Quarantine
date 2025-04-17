using System;
using System.Collections.Generic;

namespace _Quarantine.Code.Infrastructure.Services
{
	public interface IServiceContainer
	{
		public TService Get<TService>();
	}

    public class ServiceContainer : IServiceContainer
    {
	    private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
	    
	    public TService Get<TService>()
	    {
		    if (_services.TryGetValue(typeof(TService), out var service))
			    return (TService)service;
		    
		    throw new KeyNotFoundException($"No service of type {typeof(TService).Name} could be found.");
	    }

	    public void Register<TService>(TService service)
	    {
		    if (_services.ContainsKey(typeof(TService)))
			    throw new InvalidOperationException();
		    
		    _services.Add(typeof(TService), service);
	    }
    }
}