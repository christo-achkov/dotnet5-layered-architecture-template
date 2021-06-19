using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AAR.Service.Command;
using Newtonsoft.Json;
using Serilog;

namespace AAR.Service.Decoratee
{
    public class LoggingCommandDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly ICommandService<TCommand> _decoratee;

        public LoggingCommandDecorator(ICommandService<TCommand> decoratee)
        {
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
        }

        public async Task Execute(TCommand command)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Log.Information($"Command execution started with command: {JsonConvert.SerializeObject(command)}");

                await _decoratee.Execute(command);

                stopwatch.Stop();

                Log.Information($"Command execution completed successfully with command: {JsonConvert.SerializeObject(command)}, elasped time: {stopwatch.Elapsed}");
            }
            catch (Exception e)
            {
                stopwatch.Stop();

                Log.Error($"Command execution failed with command: {JsonConvert.SerializeObject(command)}, error: {e.Message}, elapsed time: {stopwatch.Elapsed}");
                throw;
            }
        }
    }
}
