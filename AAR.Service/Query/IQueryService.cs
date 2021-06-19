using System.Threading.Tasks;

namespace AAR.Service.Query
{
    public interface IQueryService<TQuery, TResult>
    {
        public Task<TResult> Execute(TQuery query);
    }
}
