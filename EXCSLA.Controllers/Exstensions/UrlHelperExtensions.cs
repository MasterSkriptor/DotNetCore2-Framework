using Microsoft.AspNetCore.Mvc;
using EXCSLA.Controllers;
using EXCSLA.Services.DataServices;
using EXCSLA.Services.EmailServices;

namespace EXCSLA.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ReplyToContactLink(this IUrlHelper urlHelper, int contactId, string scheme)
        {
            return urlHelper.Action(
                action: nameof(ContactControllerBase<IDataService, IEmailSender>.Reply),
                controller: "Contact",
                values: new { id = contactId },
                protocol: scheme);
        }
    }
}