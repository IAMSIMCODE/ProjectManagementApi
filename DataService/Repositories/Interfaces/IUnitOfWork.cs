namespace DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDeveloperRepository Developers { get; }
        IAchievementRepository Achievements { get; }

        Task<bool> CompleteAsync();
    }
}
