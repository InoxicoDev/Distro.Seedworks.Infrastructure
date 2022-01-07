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
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IDomainEventHandler<T>>();

        foreach (var handler in handlers)
        {
            handler.Handle(@event);
        }
    }
    
    public void Send<T>(T command) where T : IDomainCommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IDomainCommandHandler<T>>();
        handler.Handle(command);
    }
    
    public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IDomainRequest
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IDomainRequestHandler<TRequest, TResponse>>();
        return handler.Handle(request);
    }
}