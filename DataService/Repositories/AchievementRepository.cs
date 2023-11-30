using DataService.Data;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories
{
    public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {}

        public async Task<Achievement?> GetDeveloperAchievementAsync(Guid developerId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.DeveloperId == developerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetDeveloperAchievementAsync function error", typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<Achievement>> GetAll()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function error", typeof(AchievementRepository));
                throw;
            }
        }


        public override async Task<bool> Delete(Guid id)
        {

            try
            {
                // get the entity 
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null) { return false; }

                result.Status = 00;
                result.UpdatedDate = DateTime.Now;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Achievement achievement)
        {
            try
            {
                // get the entity 
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);

                if (result == null) { return false; }

                result.UpdatedDate = DateTime.Now;
                result.OngoingProject = achievement.OngoingProject;
                result.CompletionLevel = achievement.CompletionLevel;
                result.CompletedProject = achievement.CompletedProject;
                result.ProjectOnHold = achievement.ProjectOnHold;
                result.DeployedToProduction = achievement.DeployedToProduction;
                 
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(AchievementRepository));
                throw;
            }
        }
    }
}
