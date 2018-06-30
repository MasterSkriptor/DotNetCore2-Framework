using System.Threading.Tasks;

namespace EXCSLA.Services.EmailServices.SmtpClientServices
{
    public interface ISmtpClientService
    {
        Task SendMessageAsync(string email, string subject, string message);
        Task SendMessageAsync(string email, string subject, string message, string from);

    }
}