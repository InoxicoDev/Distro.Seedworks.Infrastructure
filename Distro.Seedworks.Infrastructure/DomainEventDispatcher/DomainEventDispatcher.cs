using Distro.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Distro.Seedwork.Infrastructure;

public class DomainEventDispatcher: IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public void Publish<T>(T @event) where T: IDomainEvent
    {
        //using var scope = _serviceProvider.CreateScope();
        var handlers = _serviceProvider.GetServices<IDomainEventHandler<T>>();
        foreach (var handler in handlers)
        {
            handler.Handle(@event);
        }
    }
    public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IDomainRequest
    {
        //using var scope = _serviceProvider.CreateScope();
        var handler = _serviceProvider.GetRequiredService<IDomainRequestHandler<TRequest, TResponse>>();
        return handler.Handle(request);
    }
}