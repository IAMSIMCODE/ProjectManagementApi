using DataService.Data;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {}


        public override async Task<IEnumerable<Developer>> GetAll()
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
                _logger.LogError(ex, "{Repo} GetAll function error", typeof(DeveloperRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(DeveloperRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Developer entity)
        {
            try
            {
                // get the entity 
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (result == null) { return false; }

                result.UpdatedDate = DateTime.Now;
                result.DevNumber = entity.DevNumber;
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.DateOfBirth = entity.DateOfBirth;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(DeveloperRepository));
                throw;
            }
        }
    }
}
