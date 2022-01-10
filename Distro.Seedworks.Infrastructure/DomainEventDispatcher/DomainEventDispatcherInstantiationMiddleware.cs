using Distro.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Distro.Seedwork.Infrastructure;

public class DomainEventDispatcherInstantiationMiddleware
{
    private readonly RequestDelegate _next;
    
    public DomainEventDispatcherInstantiationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, IDomainEventDispatcher domainEventDispatcher)
    {
        DomainEventPublisher.SetInstance(domainEventDispatcher);
        
        await _next(context);
        
        DomainEventPublisher.Cleanup();
    }
}