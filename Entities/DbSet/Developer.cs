namespace Entities.DbSet
{
    public class Developer : BaseEntity
    {
        public Developer()
        {
            Achievements = new HashSet<Achievement>();
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DevNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
