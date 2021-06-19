using AAR.Model.Command.Example;
using AAR.Model.Entity.Example;
using AAR.Repository.Example;
using System;
using System.Threading.Tasks;

namespace AAR.Service.Command.Example
{
    public class AddExampleService : ICommandService<AddExampleCommand>
    {
        private readonly IExampleRepository _exampleRepository;

        public AddExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository ?? throw new ArgumentNullException(nameof(exampleRepository));
        }

        public async Task Execute(AddExampleCommand command)
        {
            var entityToAdd = new ExampleEntity
            {
                Name = command.Name
            };

            await _exampleRepository.Add(entityToAdd);

            await _exampleRepository.SaveChanges();
        }
    }
}
