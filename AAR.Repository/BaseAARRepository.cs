using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAR.Data.Context.AAR;
using AAR.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AAR.Repository
{
    public class BaseAARRepository : IBaseAARRepository
    {
        protected readonly AARContext _context;

        public BaseAARRepository(AARContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> SaveChanges()
        {
            DateTime currentDateTime = DateTime.Now;

            IEnumerable<EntityEntry<BaseEntity>> entities = _context.ChangeTracker.Entries<BaseEntity>();

            foreach (EntityEntry<BaseEntity> entity in entities.Where(e => (e.State == EntityState.Added)))
            {
                entity.Entity.CreatedOn = currentDateTime;
                entity.Entity.ModifiedOn = currentDateTime;
            }

            foreach (EntityEntry<BaseEntity> entity in entities.Where(e => (e.State == EntityState.Modified)))
            {
                entity.Entity.ModifiedOn = currentDateTime;
            }

            return await _context.SaveChangesAsync(false);
        }
    }
}
