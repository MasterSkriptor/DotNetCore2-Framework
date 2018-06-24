using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXCSLA.Services.EmailServices.SmtpClientServices
{
    public class SmtpClientOptions
    {
        private string _hostName;
        private string _userName;
        private string _password;
        private bool _useDefaultCredentials;

        public string HostName { get { return _hostName; } set { _hostName = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public bool UseDefaultCredentials { get { return _useDefaultCredentials; } set { _useDefaultCredentials = value; } }

        public SmtpClientOptions()
        {

        }
    }
}
