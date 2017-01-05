namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileRegistration
    {
        Gender Gender { get; }
        byte[] ProfileImage { get; }
    }
}