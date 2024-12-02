namespace ApiLogin.Servicio
{
    public interface IEmailServicio
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
        public string GeneratePasswordResetToken(string email);
    }
}
