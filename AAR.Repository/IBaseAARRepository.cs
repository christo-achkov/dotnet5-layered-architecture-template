using System.Threading.Tasks;

namespace AAR.Repository
{
    public interface IBaseAARRepository
    {
        Task<int> SaveChanges();
    }
}
