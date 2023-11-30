using Entities.DbSet;

namespace DataService.Repositories.Interfaces
{
    public interface IAchievementRepository : IGenericRepository<Achievement>
    {
        Task<Achievement?> GetDeveloperAchievementAsync(Guid developerId);
    }
}
