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
        // Doing a SetInstance here for a static class when this gets executed in parallel across multiple threads is
        // a problem.
        DomainEventPublisher.SetInstance(domainEventDispatcher);
        
        await _next(context);
        
        DomainEventPublisher.Cleanup();
    }
}