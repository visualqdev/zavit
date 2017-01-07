namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileRegistration
    {
        Gender Gender { get; }
        byte[] ProfileImage { get; }
        string Email { get; }
        string DisplayName { get; }
    }
}