namespace app.advertise.dtos.Vendor
{
    public class dtoOTPPasswordReset
    {
        public int OTP { get;set; }
        public string Password { get;set; }
        public string ConfirmPassword { get;set; }
        public string EmailLink { get;set; }
        public string UserId { get;set; }
        public string UserEmailId { get;set; }
        public string UlbId { get;set; } 
    }

    public class dtoOTPPasswordResponse
    {
       public bool PasswordReset { get;set; }   
    }
}
