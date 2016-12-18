namespace zavit.Domain.Profiles.Updating
{
    public interface IProfileUpdater
    {
        bool Update(Profile profile, ProfileUpdate profileUpdate);
    }
}