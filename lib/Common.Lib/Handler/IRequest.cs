namespace Common.Lib.Handler;
public interface IRequest
{

}

public interface IRequest<out TResponse> where TResponse : Result
{
}
