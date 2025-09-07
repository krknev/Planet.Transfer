namespace Common.Lib.Handler;
public interface IMediator
{
    Task Send(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TResponse>(IRequest<Result<TResponse>> request, CancellationToken cancellationToken = default);
}

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        var handler = serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle");
        if (method == null)
            throw new InvalidOperationException($"Handler {handler.GetType().Name} does not contain a valid Handle method");

        var task = (Task)method.Invoke(handler, new object[] { request, cancellationToken })!;
        await task;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<Result<TResponse>> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();

        // Get the IRequest<T> interface implemented on the request
        var interfaceType = requestType
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));

        if (interfaceType == null)
            throw new InvalidOperationException($"Request type {requestType.Name} does not implement IRequest<>");

        var responseType = interfaceType.GetGenericArguments()[0]; // Result<TResponse>

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for {requestType.Name}");

        var method = handlerType.GetMethod("Handle");
        if (method == null)
            throw new InvalidOperationException($"Handler {handler.GetType().Name} does not contain a valid Handle method");

        var resultTask = method.Invoke(handler, new object[] { request, cancellationToken })!;
        var result = await (dynamic)resultTask;

        return (TResponse)((dynamic)result).Value!; // assuming Result<T> has a .Value
    }

}

