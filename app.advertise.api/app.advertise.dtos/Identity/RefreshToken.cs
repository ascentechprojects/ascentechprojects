using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.advertise.dtos.Identity
{
        public class RefreshToken
        {
            public string Token { get; set; }

            //public DateTime Expires { get; set; }

            //public DateTime Created { get; set; }

            //public DateTime? Revoked { get; set; }


            //[NotMapped]
            //public bool IsExpired => DateTime.UtcNow >= Expires;

            //[NotMapped]
            //public bool IsRevoked => Revoked != null;

            //[NotMapped]
            //public bool IsActive => !IsRevoked && !IsExpired;
        }


}
