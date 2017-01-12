namespace zavit.Domain.Profiles
{
    public interface IProfileImageRepository
    {
        ProfileImage Get(int accountId);
    }
}