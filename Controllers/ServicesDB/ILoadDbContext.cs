using UnicdaPlatform.Data;

namespace UnicdaPlatform.Controllers.ServicesDB
{
    public interface ILoadDbContext
    {
        public ApplicationDbContext GetConnection();
    }
}
