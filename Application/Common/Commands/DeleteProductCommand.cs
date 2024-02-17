namespace Application.Common.Commands
{
    public class DeleteProductCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}
