namespace zavit.Domain.Profiles
{
    public interface IProfileRepository
    {
        void Update(Profile profile);
        Profile GetForAccount(int accountId);
        void Save(Profile profile);
    }
}