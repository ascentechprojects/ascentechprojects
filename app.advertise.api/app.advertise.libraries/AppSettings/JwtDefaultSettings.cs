using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.advertise.libraries.AppSettings
{ 
    public class JwtDefaultSetting
    {
        public string Issuer { get; set; }
      
        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public string AccessTokenTTLInMins { get; set; }
        
        public string RefreshTokenTTLInDays { get; set; }
    }
}
