using DataService.Data;
using DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IDeveloperRepository Developers { get; }
        public IAchievementRepository Achievements { get; }

        public UnitOfWork(ILoggerFactory loggerFactory, AppDbContext context)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");


            Developers = new DeveloperRepository(logger, _context);
            Achievements = new AchievementRepository(logger, _context);
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0; 
        }

        public void Dispose() { _context.Dispose(); }
    }
}
