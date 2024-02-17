namespace Application.Common.Handlers
{
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}
