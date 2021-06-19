using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AAR.Service.Query;
using Newtonsoft.Json;
using Serilog;

namespace AAR.Service.Decoratee
{
    public class LoggingQueryDecorator<TQuery, TResult> : IQueryService<TQuery, TResult>
    {
        private readonly IQueryService<TQuery, TResult> _decoratee;

        public LoggingQueryDecorator(IQueryService<TQuery, TResult> decoratee)
        {
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
        }

        public async Task<TResult> Execute(TQuery query)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Log.Information($"Query execution started with query: {JsonConvert.SerializeObject(query)}");

                var result = await _decoratee.Execute(query);

                stopwatch.Stop();

                Log.Information($"Query execution completed successfully with query: {JsonConvert.SerializeObject(query)}, result: {JsonConvert.SerializeObject(result)}, elapsed time: {stopwatch.Elapsed}");

                return result;
            }
            catch (Exception e)
            {
                stopwatch.Stop();

                Log.Error($"Query execution failed with query: {JsonConvert.SerializeObject(query)}, error: {e.Message}, elapsed time {stopwatch.Elapsed}");

                throw;
            }
        }
    }
}
