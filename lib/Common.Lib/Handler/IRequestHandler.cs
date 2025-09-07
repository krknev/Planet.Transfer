namespace Common.Lib.Handler;
public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
}

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}