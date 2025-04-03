using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SettingsRequestDto
    {
        public string SecretKey { get; set; }
        public string UserToken { get; set; }
        public string PasswordToken { get; set; }

    }
}
