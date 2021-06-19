using System;
using System.Threading.Tasks;
using AAR.Data.Context.AAR;
using AAR.Service.Command;

namespace ARR.Service.Decoratee
{
    public class TransactionCommandDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly ICommandService<TCommand> _decoratee;
        private readonly AARContext _context;

        public TransactionCommandDecorator(ICommandService<TCommand> decoratee, AARContext context)
        {
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Execute(TCommand command)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _decoratee.Execute(command);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Dispose();

                    throw;
                }
            }
        }
    }
}
