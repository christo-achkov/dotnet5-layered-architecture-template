using AAR.Model.Query.Example;
using AAR.Repository.Example;
using System;
using System.Threading.Tasks;

namespace AAR.Service.Query.Example
{
    public class GetExampleByIdService : IQueryService<GetExampleByIdQuery, GetExampleByIdResult>
    {
        private readonly IExampleRepository _exampleRepository;

        public GetExampleByIdService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository ?? throw new ArgumentNullException(nameof(exampleRepository));
        }

        public async Task<GetExampleByIdResult> Execute(GetExampleByIdQuery query)
        {
            var exampleEntity = await _exampleRepository.GetById(query.Id);

            // just for quick testing purposes
            var result = new GetExampleByIdResult
            {
                Name = exampleEntity?.Name,
            };

            return result;
        }
    }
}
