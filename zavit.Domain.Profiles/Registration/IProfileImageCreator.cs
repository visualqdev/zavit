namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileImageCreator
    {
        ProfileImage Create(IAccountProfileRegistration accountProfileRegistration);
    }
}