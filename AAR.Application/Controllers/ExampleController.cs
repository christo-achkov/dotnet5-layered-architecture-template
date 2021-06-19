
using System.Threading.Tasks;
using AAR.Model.Command.Example;
using AAR.Model.Query.Example;
using AAR.Service.Command;
using AAR.Service.Query;
using Microsoft.AspNetCore.Mvc;

namespace AAR.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly IQueryService<GetExampleByIdQuery, GetExampleByIdResult> _getExampleByIdService;
        private readonly ICommandService<AddExampleCommand> _addExampleService;

        public ExampleController(
            IQueryService<GetExampleByIdQuery, GetExampleByIdResult> getExampleByIdService,
            ICommandService<AddExampleCommand> addExampleService)
        {
            _getExampleByIdService = getExampleByIdService;
            _addExampleService = addExampleService;
        }

        [HttpGet]
        public async Task<GetExampleByIdResult> GetExampleById(GetExampleByIdQuery query)
        {
            return await _getExampleByIdService.Execute(query);
        }

        [HttpPost]
        public async Task AddExample(AddExampleCommand command)
        {
            await _addExampleService.Execute(command);
        }
    }
}