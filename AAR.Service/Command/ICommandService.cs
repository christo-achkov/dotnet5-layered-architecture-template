using System.Threading.Tasks;

namespace AAR.Service.Command
{
    public interface ICommandService<TCommand>
    {
        public Task Execute(TCommand command);
    }
}
