using System;
using System.Threading.Tasks;
using AAR.Data.Context.AAR;
using AAR.Model.Entity.Example;
using Microsoft.EntityFrameworkCore;

namespace AAR.Repository.Example
{
    public class ExampleRepository : BaseAARRepository, IExampleRepository
    {
        public ExampleRepository(AARContext context) : base(context) { }

        public async Task Add(ExampleEntity example)
        {
            await _context.Examples.AddAsync(example);
        }

        public async Task<ExampleEntity> GetById(Guid id)
        {
            return await _context.Examples.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
