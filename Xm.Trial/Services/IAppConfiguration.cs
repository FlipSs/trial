namespace Xm.Trial.Services
{
    public interface IAppConfiguration
    {
        MailData EmailData { get; }
        ContactUsFormData ContactUsData { get; }
    }
}
