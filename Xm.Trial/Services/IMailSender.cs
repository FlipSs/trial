using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xm.Trial.Services
{
    public interface IMailSender
    {
        Task SendEmailAsync(string subject, string messageBody, bool isHtmlBody, List<string> attachmentsPaths);
    }
}
