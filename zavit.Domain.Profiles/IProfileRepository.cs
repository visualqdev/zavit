namespace zavit.Domain.Profiles
{
    public interface IProfileRepository
    {
        void Update(Profile profile);
        void Save(Profile profile);
    }
}