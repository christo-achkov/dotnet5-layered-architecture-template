using System;
using System.Threading.Tasks;
using AAR.Model.Entity.Example;

namespace AAR.Repository.Example
{
    public interface IExampleRepository : IBaseAARRepository
    {
        Task Add(ExampleEntity entity);
        Task<ExampleEntity> GetById(Guid id);
    }
}
